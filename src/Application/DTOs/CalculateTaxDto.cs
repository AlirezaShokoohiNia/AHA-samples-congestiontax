namespace AHA.CongestionTax.Application.Dtos
{
    /// <summary>
    /// DTO used to request a congestion tax calculation.
    /// </summary>
    public sealed class CalculateTaxDto
    {
        /// <summary>
        /// The date for which tax must be calculated.
        /// </summary>
        public DateOnly Date { get; init; }

        /// <summary>
        /// The vehicle's license plate identifier.
        /// </summary>
        public string LicensePlate { get; init; } = default!;
    }
}
