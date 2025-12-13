namespace AHA.CongestionTax.Api.Endpoints
{
    using AHA.CongestionTax.Application.Abstractions.Command;
    using AHA.CongestionTax.Application.Commands;

    public static class PassApiEndpoints
    {
        public static void Map(WebApplication app)
        {
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

        }
    }
}
