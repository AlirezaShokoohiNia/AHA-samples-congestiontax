namespace AHA.CongestionTax.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using AHA.CongestionTax.Domain.Abstractions;
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.VehicleAgg;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// EF Core DbContext for congestion tax system.
    /// Contains DbSets for all write-side aggregates.
    /// </summary>
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options), IUnitOfWork
    {
        public DbSet<DayToll> DayTolls => Set<DayToll>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();

        #region IUnitOfWork

        public async Task<Result<int>> CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var changes = await SaveChangesAsync(cancellationToken);
                return Result.Success(changes);
            }
            catch (Exception ex)
            {
                return Result.Failure<int>(ex.Message);
            }
        }

        public Task<Result> RollbackAsync(CancellationToken cancellationToken = default)
        {
            // EF Core does not track rollback automatically.
            // Optional: implement via Transaction if needed.
            return Task.FromResult(Result.Success());
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all IEntityTypeConfiguration<T> in this assembly
            _ = modelBuilder.ApplyConfigurationsFromAssembly(
                    typeof(AppDbContext).Assembly,
                    t => t.Namespace != null && t.Namespace.Contains("AHA.CongestionTax.Infrastructure.Data.Configurations"));

        }
    }
}
