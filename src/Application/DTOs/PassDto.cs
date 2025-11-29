namespace AHA.CongestionTax.Application.Dtos
{
    /// <summary>
    /// DTO representing a single pass event at a checkpoint.
    /// </summary>
    public sealed class PassDto
    {
        /// <summary>
        /// Exact time when the vehicle passed the checkpoint.
        /// </summary>
        public DateTime Timestamp { get; init; }

        /// <summary>
        /// The license plate identifier of the vehicle passing.
        /// </summary>
        public string LicensePlate { get; init; } = default!;
    }
}
