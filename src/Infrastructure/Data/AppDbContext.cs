namespace AHA.CongestionTax.Infrastructure.Data
{
    using Microsoft.EntityFrameworkCore;
    using AHA.CongestionTax.Domain.Abstractions;
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.VehicleAgg;
    using System.Threading;

    /// <summary>
    /// EF Core DbContext for Sample01.
    /// Contains DbSets for all write-side aggregates used in the congestion tax system.
    /// Configurations are minimal, for scaffolding and TDD testing.
    /// </summary>
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : DbContext(options), IUnitOfWork
    {
        public DbSet<DayToll> DayTolls => Set<DayToll>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();

        #region IUnitOfWork

        public async Task<int> CommitAsync() => await SaveChangesAsync();

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
                    => await SaveChangesAsync(cancellationToken);

        public Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            // EF Core does not track rollback automatically.
            // Optional: implement via Transaction if needed.
            return Task.CompletedTask;
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all IEntityTypeConfiguration<T> in this assembly
            _ = modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}