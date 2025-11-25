namespace AHA.CongestionTax.Domain.Abstractions
{
    /// <summary>
    /// Base interface for repository implementations that manage persistence
    /// for a specific aggregate root.
    /// </summary>
    /// <typeparam name="TAggRoot">Aggregate root type.</typeparam>
    public interface IRepository<TAggRoot>
        where TAggRoot : AggregateRoot
    {
        /// <summary>
        /// Gets the associated Unit of Work for this repository.
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        #region Read Operations

        /// <summary>
        /// Asynchronously finds an aggregate by its identity.
        /// </summary>
        /// <param name="id">Identity of the aggregate.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// The aggregate instance, or <see langword="null"/> if not found.
        /// </returns>
        ValueTask<TAggRoot?> FindByIdAsync(int id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves all aggregates of type <typeparamref name="TAggRoot"/>.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// A list of aggregate instances.
        /// </returns>
        Task<List<TAggRoot>> FindAllAsync(
            CancellationToken cancellationToken = default);

        #endregion

        #region Write Operations

        /// <summary>
        /// Adds a new aggregate to the repository.
        /// </summary>
        ValueTask AddAsync(TAggRoot aggregate,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes an aggregate by its identifier.
        /// </summary>
        ValueTask RemoveAsync(int aggregateRootId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Marks an aggregate as modified within the current Unit of Work.
        /// </summary>
        ValueTask ModifyAsync(TAggRoot aggregate,
            CancellationToken cancellationToken = default);

        #endregion
    }
}