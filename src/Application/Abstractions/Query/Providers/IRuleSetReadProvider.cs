namespace AHA.CongestionTax.Application.Abstractions.Query.Providers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.DTOs;

    /// <summary>
    /// Defines the read provider contract for retrieving rule sets
    /// from sources.
    /// </summary>
    public interface IRuleSetReadProvider
    {
        /// <summary>
        /// Asynchronously retrieves the rule set for a given city.
        /// </summary>
        /// 
        /// <param name="city">City name to query rules for.</param>
        /// 
        /// <returns>
        /// A <see cref="QueryResult{T}"/> containing the <see cref="RuleSetReadModel"/> if found,
        /// or a failure result with an error message if not found or query fails.
        /// </returns>
        Task<QueryResult<RuleSetDto>> GetRulesForCityAsync(
            string city,
            CancellationToken cancellationToken = default);
    }
}
