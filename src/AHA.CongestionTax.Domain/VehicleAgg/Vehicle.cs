namespace AHA.CongestionTax.Domain.VehicleAgg
{
    using AHA.CongestionTax.Domain.ValueObjects;

    /// <summary>
    /// Represents a registered vehicle in the congestion tax domain.
    /// </summary>
    /// <remarks>
    /// The aggregate root for all vehicle-related behavior.  
    /// Holds identity (license plate) and category (vehicle type).  
    /// No business logic is included at this stage.
    /// </remarks>
    public class Vehicle : AggregateRoot
    {
        /// <summary>
        /// Unique license plate identifier for the vehicle.
        /// </summary>
        public string LicensePlate { get; private set; } = default!;

        /// <summary>
        /// The classification of the vehicle used for tax decisions.
        /// </summary>
        public VehicleType VehicleType { get; private set; }

        /// <summary>
        /// Required by EF Core and serializers.
        /// </summary>
        private Vehicle() { }

        /// <summary>
        /// Creates a new vehicle instance with a license plate and type.
        /// </summary>
        /// <param name="licensePlate">The plate number identifying the vehicle.</param>
        /// <param name="vehicleType">The vehicle's category.</param>
        public Vehicle(string licensePlate, VehicleType vehicleType)
        {
            LicensePlate = licensePlate;
            VehicleType = vehicleType;
        }
    }
}
