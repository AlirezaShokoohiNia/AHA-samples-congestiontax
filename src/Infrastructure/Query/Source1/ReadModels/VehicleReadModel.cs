namespace AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels
{

    /// <summary>
    /// Read model representing vehicle aggregate.
    /// </summary>
    public sealed class VehicleReadModel
    {
        /// <summary>
        /// Unique persistence identifier of the vehicle in the database.
        /// </summary>
        public int VehicleId { get; set; }

        /// <summary>
        /// Unique license plate identifier of the vehicle.
        /// </summary>
        public string LicensePlate { get; set; } = default!;

        /// <summary>
        /// Category of the vehicle (car, bus, emergency, etc.).
        /// </summary>
        public VehicleTypeReadModel VehicleType { get; set; } = default!;
    }
}