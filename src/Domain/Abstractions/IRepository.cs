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
        /// A <see cref="Result{T}"/> containing the aggregate instance if found,
        /// or a failure result with an error message if not found.
        /// </returns>
        ValueTask<Result<TAggRoot>> FindByIdAsync(int id,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously retrieves all aggregates of type <typeparamref name="TAggRoot"/>.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> containing the list of aggregate instances,
        /// or a failure result if retrieval fails.
        /// </returns>
        Task<Result<List<TAggRoot>>> FindAllAsync(
            CancellationToken cancellationToken = default);

        #endregion

        #region Write Operations

        /// <summary>
        /// Adds a new aggregate to the repository.
        /// </summary>
        /// <param name="aggregate">Aggregate root instance to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure of the operation.
        /// </returns>
        ValueTask<Result> AddAsync(TAggRoot aggregate,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Removes an aggregate by its identifier.
        /// </summary>
        /// <param name="aggregateRootId">Identifier of the aggregate root.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure of the operation.
        /// </returns>
        ValueTask<Result> RemoveAsync(int aggregateRootId,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Marks an aggregate as modified within the current Unit of Work.
        /// </summary>
        /// <param name="aggregate">Aggregate root instance to modify.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure of the operation.
        /// </returns>
        ValueTask<Result> ModifyAsync(TAggRoot aggregate,
            CancellationToken cancellationToken = default);

        #endregion
    }
}