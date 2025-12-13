namespace AHA.CongestionTax.Api
{
    using AHA.CongestionTax.Application.Abstractions.Command;
    using AHA.CongestionTax.Application.Abstractions.Query;
    using AHA.CongestionTax.Application.Commands;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Application.Queries;

    [Obsolete("This class is deprecated. Use specific endpoint classes instead.")]
    public static class ApiEndpoints
    {
        public static void Map(WebApplication app)
        {
            // Register a vehicle
            app.MapPost("/vehicles", async (
                RegisterVehicleCommand command,
                ICommandHandler<RegisterVehicleCommand, int> handler,
                CancellationToken ct) =>
            {
                var result = await handler.HandleAsync(command, ct);
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.BadRequest(result.Error);
            });

            // Register a pass
            app.MapPost("/passes", async (
                RegisterPassCommand command,
                ICommandHandler<RegisterPassCommand, int> handler,
                CancellationToken ct) =>
            {
                var result = await handler.HandleAsync(command, ct);
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.BadRequest(result.Error);
            });

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

            // Get vehicle by plate
            app.MapGet("/vehicles/{plate}", async (
                string plate,
                IQueryHandler<GetVehicleQuery, VehicleDto> handler,
                CancellationToken ct) =>
            {
                var query = new GetVehicleQuery(plate);
                var result = await handler.HandleAsync(query, ct);
                return result.IsSuccess
                    ? Results.Ok(result.Value)
                    : Results.NotFound(result.Error);
            });

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