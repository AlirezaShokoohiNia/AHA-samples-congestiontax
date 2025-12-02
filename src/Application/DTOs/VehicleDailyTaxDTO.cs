namespace AHA.CongestionTax.Application.DTOs
{

    /// <summary>
    /// Represents the daily tax amount for a vehicle in a specific city.
    /// </summary>
    public sealed class VehicleDailyTaxDto
    {
        /// <summary>
        /// Unique license plate identifier of the vehicle.
        /// </summary>
        public string LicensePlate { get; init; } = default!;

        /// <summary>
        /// The date of the toll.
        /// </summary>
        public DateOnly Date { get; init; }

        /// <summary>
        /// The city where the toll was recorded.
        /// </summary>
        public string City { get; init; } = default!;

        /// <summary>
        /// The tax amount for that day in the city.
        /// </summary>
        public int Tax { get; init; }
    }
}
