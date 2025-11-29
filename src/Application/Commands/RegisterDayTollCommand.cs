namespace AHA.CongestionTax.Application.Commands
{
    using AHA.CongestionTax.Application.Dtos;

    /// <summary>
    /// Command for registering all passes for a vehicle on a specific day.
    /// </summary>
    public sealed record RegisterDayTollCommand(
        string LicensePlate,
        DateOnly Date,
        IReadOnlyCollection<PassDto> Passes
    );
}