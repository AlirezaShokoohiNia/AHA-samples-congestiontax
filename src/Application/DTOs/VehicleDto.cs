namespace AHA.CongestionTax.Application.DTOs
{
    /// <summary>
    /// Read-side data transfer object representing a vehicle,
    /// used by CQRS queries and their handlers.
    /// </summary>
    public sealed class VehicleDto
    {
        /// <summary>
        /// Unique persistence identifier of the vehicle in the database.
        /// </summary>
        public int VehicleId { get; set; }

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
