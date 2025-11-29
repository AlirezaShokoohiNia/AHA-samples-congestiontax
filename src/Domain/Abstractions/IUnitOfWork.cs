namespace AHA.CongestionTax.Domain.Abstractions
{

    /// <summary>
    /// Represents the Unit of Work pattern, coordinating changes across repositories.
    /// </summary>
    /// <remarks>
    /// A Unit of Work tracks changes and persists them as a single transaction.
    /// If <see cref="CommitAsync"/> fails, all changes should be reverted via transaction rollback.
    /// </remarks>
    public interface IUnitOfWork : IAsyncDisposable
    {
        /// <summary>
        /// Asynchronously commits all tracked changes to the data store.
        /// </summary>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        /// <returns>
        /// A <see cref="Result{T}"/> containing the number of state entries written to the database
        /// if successful, or a failure result with an error message if persistence fails.
        /// </returns>
        Task<Result<int>> CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously rolls back all tracked changes.
        /// </summary>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        /// <returns>
        /// A <see cref="Result"/> indicating success or failure of the rollback operation.
        /// </returns>
        Task<Result> RollbackAsync(CancellationToken cancellationToken = default);
    }
}

