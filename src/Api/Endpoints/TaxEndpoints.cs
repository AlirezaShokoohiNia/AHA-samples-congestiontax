namespace AHA.CongestionTax.Api.Endpoints
{
    using AHA.CongestionTax.Application.Abstractions.Query;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Application.Queries;

    public static partial class TaxEndpoints
    {
        public static void Map(WebApplication app)
        {
            // Get daily tax records
            app.MapGet("/vehicles/{id}/tax-records", async (
                int id,
                DateOnly from,
                DateOnly to,
                IQueryHandler<GetVehicleDailyTaxRecordsQuery, IReadOnlyCollection<VehicleDailyTaxDto>> handler,
                CancellationToken ct) =>
            {
                var query = new GetVehicleDailyTaxRecordsQuery(id, from, to);
                var result = await handler.HandleAsync(query, ct);
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.NotFound(result.Error);
            });

            // Get weekly total tax
            app.MapGet("/vehicles/{id}/weekly-tax", async (
                int id,
                IQueryHandler<GetVehicleWeeklyTotalTaxQuery, VehicleTotalTaxDto> handler,
                CancellationToken ct) =>
            {
                var query = new GetVehicleWeeklyTotalTaxQuery(id);
                var result = await handler.HandleAsync(query, ct);
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.NotFound(result.Error);
            });
        }

    }
}