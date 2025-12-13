namespace AHA.CongestionTax.Api.Endpoints
{
    using AHA.CongestionTax.Application.Abstractions.Query;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Application.Queries;

    public static class RuleApiEndpoints
    {
        public static void Map(WebApplication app)
        {
            // Get rule set
            app.MapGet("/rules/{city}", async (
                string city,
                IQueryHandler<GetRuleSetQuery, RuleSetDto> handler,
                CancellationToken ct) =>
            {
                var query = new GetRuleSetQuery(city);
                var result = await handler.HandleAsync(query, ct);
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.NotFound(result.Error);
            });
        }
    }
}