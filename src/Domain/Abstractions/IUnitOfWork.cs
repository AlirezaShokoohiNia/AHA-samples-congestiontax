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
        /// The number of state entries written to the database.
        /// </returns>
        /// <exception cref="Exception">
        /// Thrown if persistence fails.  
        /// The transaction must rollback in this case.
        /// </exception>
        Task<int> CommitAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously rolls back all tracked changes.
        /// </summary>
        /// <remarks>
        /// Rollback behavior depends on the underlying ORM/transaction provider.
        /// </remarks>
        Task RollbackAsync(CancellationToken cancellationToken = default);
    }
}