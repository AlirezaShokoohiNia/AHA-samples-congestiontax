namespace AHA.CongestionTax.Application.DTOs
{
    /// <summary>
    /// Data transfer object representing a vehicle to be registered
    /// through a write-side command.
    /// </summary>
    public sealed class VehicleDto
    {
        /// <summary>
        /// Unique license plate identifier of the vehicle.
        /// </summary>
        public string LicensePlate { get; init; } = default!;

        /// <summary>
        /// Category of the vehicle (car, bus, emergency, etc.).
        /// </summary>
        public string VehicleType { get; init; } = default!;
    }
}