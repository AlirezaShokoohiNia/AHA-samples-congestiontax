namespace AHA.CongestionTax.Application.Abstractions.Queries
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the contract for handling queries in the CQRS pattern.
    /// A query handler is responsible for executing a query and returning
    /// a <see cref="QueryResult{TResult}"/> containing the requested data.
    /// </summary>
    /// <typeparam name="TQuery">
    /// The type of query to be handled. Must implement <see cref="IQuery"/>.
    /// </typeparam>
    /// <typeparam name="TResult">
    /// The type of result returned by the query handler.
    /// </typeparam>
    public interface IQueryHandler<in TQuery, TResult>
        where TQuery : IQuery
    {
        /// <summary>
        /// Executes the specified query asynchronously and returns the result.
        /// </summary>
        /// <param name="query">The query instance containing the request parameters.</param>
        /// <param name="cancellationToken">
        /// A token to monitor for cancellation requests.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains
        /// a <see cref="QueryResult{TResult}"/> with the data retrieved by the query.
        /// </returns>
        Task<QueryResult<TResult>> HandleAsync(
            TQuery query,
            CancellationToken cancellationToken = default);
    }
}
