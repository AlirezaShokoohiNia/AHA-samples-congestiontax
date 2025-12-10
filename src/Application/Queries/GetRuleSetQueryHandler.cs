namespace AHA.CongestionTax.Application.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions.Query;
    using AHA.CongestionTax.Application.Abstractions.Query.Providers;
    using AHA.CongestionTax.Application.DTOs;

    /// <summary>
    /// Handles the <see cref="GetRuleSetQuery"/> by delegating to the read provider
    /// and returning the resulting <see cref="QueryResult{T}"/>.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the handler with the specified provider.
    /// </remarks>
    /// <param name="ruleSetReadProvider">The rule sets read provider.</param>
    public class GetRuleSetQueryHandler(IRuleSetReadProvider ruleSetReadProvider)
            : IQueryHandler<GetRuleSetQuery, RuleSetDto>
    {

        /// <inheritdoc />
        public Task<QueryResult<RuleSetDto>> HandleAsync(
            GetRuleSetQuery query,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);
            return ruleSetReadProvider.GetRulesForCityAsync(query.City, cancellationToken);
        }
    }
}
