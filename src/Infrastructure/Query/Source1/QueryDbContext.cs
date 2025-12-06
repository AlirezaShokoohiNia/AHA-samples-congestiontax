namespace AHA.CongestionTax.Infrastructure.Query.Source1
{
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;
    using Microsoft.EntityFrameworkCore;

    using System.Linq;


    /// <summary>
    /// Represents the query-side <see cref="DbContext"/> for CQRS read models.
    /// Optimized exclusively for read operations.
    /// </summary>
    /// <remarks>
    /// Tracking is disabled globally (<see cref="QueryTrackingBehavior.NoTracking"/>)
    /// to reduce memory usage and improve performance for read-only queries.
    /// In an enterprise application, all save methods (<see cref="DbContext.SaveChanges"/> 
    /// and <see cref="DbContext.SaveChangesAsync"/>) should be overridden to throw 
    /// <see cref="InvalidOperationException"/> in order to enforce a strict 
    /// read-only contract.
    /// </remarks>
    public class QueryDbContext : DbContext, IQueryDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryDbContext"/>.
        /// </summary>
        /// <param name="options">
        /// The <see cref="DbContextOptions{TContext}"/> used to configure the context.
        /// </param>
        public QueryDbContext(DbContextOptions<QueryDbContext> options)
            : base(options)
        {
            // Disable tracking for all queries in this context.
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #region IQueryDbContext

        /// <summary>
        /// Gets the queryable source of <see cref="VehicleReadModel"/> entities.
        /// </summary>
        /// <remarks>
        /// This property exposes the vehicle read model as an <see cref="IQueryable{T}"/>,
        /// intended for LINQ-based queries. It must not be used for insert, update,
        /// or delete operations.
        /// </remarks>
        public IQueryable<VehicleReadModel> Vehicles => Set<VehicleReadModel>();

        #endregion

        /// <summary>
        /// Applies entity configurations for all read models defined in this assembly.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure entity mappings.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(typeof(QueryDbContext).Assembly);
    }

}
