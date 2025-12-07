namespace AHA.CongestionTax.Application.Queries
{
    using AHA.CongestionTax.Application.Abstractions.Query;

    /// <summary>
    /// Represents a query to retrieve rule sets by city.
    /// </summary>
    /// <param name="City">City name to rules for.</param>
    public sealed record GetRuleSetQuery(string City) : IQuery;
}
