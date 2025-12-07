namespace AHA.CongestionTax.Application.Commands
{
    using AHA.CongestionTax.Application.Abstractions.Command;

    /// <summary>
    /// Command used to register a new vehicle in the system.
    /// </summary>
    public sealed record RegisterVehicleCommand(
        string LicensePlate,
        string VehicleType
        ) : ICommand;
}