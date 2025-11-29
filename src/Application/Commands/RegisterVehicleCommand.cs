namespace AHA.CongestionTax.Application.Commands
{
    /// <summary>
    /// Command used to register a new vehicle in the system.
    /// </summary>
    public sealed record RegisterVehicleCommand(
        string LicensePlate,
        string VehicleType
    );
}