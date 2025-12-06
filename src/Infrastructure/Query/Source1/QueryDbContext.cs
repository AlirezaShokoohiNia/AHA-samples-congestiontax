namespace AHA.CongestionTax.Infrastructure.Query.Source1
{
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// DbContext for query-side read models. Optimized for read operations.
    /// </summary>
    /// <remarks>
    /// Disable tracking, will reduce memeory usage, but in the entreprise app, 
    /// every saving method should be override and throw exception to have a real
    /// query side.
    /// </remarks>
    public class QueryDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the query DbContext.
        /// </summary>
        /// <param name="options">The DbContext options.</param>
        public QueryDbContext(DbContextOptions<QueryDbContext> options)
            : base(options)
        {
            //There is no need to tracking in just query dbcontext
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        /// <summary>
        /// Read model set for vehicle aggregation.
        /// </summary>
        public DbSet<VehicleReadModel> Vehicles => Set<VehicleReadModel>();

        /// <summary>
        /// Applies read model configurations.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.ApplyConfigurationsFromAssembly(
                    typeof(QueryDbContext).Assembly,
                    t => t.Namespace != null && t.Namespace.Contains("AHA.CongestionTax.Infrastructure.Query.Source1.Configurations"));
        }
    }
}
