namespace AHA.CongestionTax.Infrastructure.Query.Source1
{
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;

    using System.Linq;

    /// <summary>
    /// Represents the read-only query contract for the CQRS read side.
    /// Provides access to <see cref="VehicleReadModel"/> entities
    /// as an <see cref="IQueryable{T}"/> source for LINQ-based queries.
    /// </summary>
    /// <remarks>
    /// This interface abstracts the underlying query infrastructure
    /// (e.g., EF Core <c>DbContext</c>) to enforce dependency inversion
    /// and improve testability. Consumers should use this contract
    /// instead of directly depending on the concrete <c>QueryDbContext</c>.
    /// 
    /// The returned <see cref="IQueryable{T}"/> is configured with
    /// <c>QueryTrackingBehavior.NoTracking</c> to ensure entities are
    /// not tracked, aligning with CQRS read-side semantics.
    /// </remarks>
    public interface IQueryDbContext
    {
        /// <summary>
        /// Gets the queryable source of <see cref="VehicleReadModel"/> entities.
        /// </summary>
        /// <remarks>
        /// Use LINQ operators (e.g., <c>Where</c>, <c>FirstOrDefaultAsync</c>)
        /// to compose queries against the vehicle read model.
        /// 
        /// This property is read-only and should never be used for
        /// insert, update, or delete operations.
        /// </remarks>
        IQueryable<VehicleReadModel> Vehicles { get; }
    }

}
