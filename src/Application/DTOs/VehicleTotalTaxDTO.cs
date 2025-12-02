namespace AHA.CongestionTax.Application.DTOs
{
    /// <summary>
    /// Represents the total tax amount for a vehicle over a period.
    /// </summary>
    public sealed class VehicleTotalTaxDto
    {
        /// <summary>
        /// Unique license plate identifier of the vehicle.
        /// </summary>
        public string LicensePlate { get; init; } = default!;

        /// <summary>
        /// The total tax amount accumulated.
        /// </summary>
        public int TotalTax { get; init; }
    }

}
