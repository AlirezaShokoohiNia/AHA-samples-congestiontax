namespace AHA.CongestionTax.Api.Endpoints
{
    using AHA.CongestionTax.Application.Abstractions.Command;
    using AHA.CongestionTax.Application.Abstractions.Query;
    using AHA.CongestionTax.Application.Commands;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Application.Queries;

    public static class VehicleEndpoints
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


        }
    }
}