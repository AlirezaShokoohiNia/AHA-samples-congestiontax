namespace AHA.CongestionTax.Application.ReadModels.Queries
{
    using AHA.CongestionTax.Application.Abstractions;
    using AHA.CongestionTax.Application.ReadModels.Queries.RuleSets;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines query operations for retrieving rule sets in read models.
    /// </summary>
    public interface IRuleSetQueries
    {
        /// <summary>
        /// Asynchronously retrieves the rule set for a given city.
        /// </summary>
        /// <param name="city">City name to query rules for.</param>
        /// <returns>
        /// A <see cref="ReadModelsQueryResult{T}"/> containing the <see cref="RuleSetReadModel"/> if found,
        /// or a failure result with an error message if not found or query fails.
        /// </returns>
        Task<ReadModelsQueryResult<RuleSetReadModel>> GetRulesForCityAsync(string city);
    }
}
