namespace AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels
{
    /// <summary>
    /// Read model representing a daily toll record.
    /// </summary>
    public sealed class DayTollReadModel
    {
        /// <summary>
        /// Unique persistence identifier of the vehicle in the database.
        /// </summary>
        public int VehicleId { get; init; }

        /// <summary>
        /// Unique license plate identifier for the vehicle.
        /// </summary>
        public string LicensePlate { get; init; } = default!;

        /// <summary>
        /// The calendar date for which tolls are being aggregated.
        /// </summary>
        public DateOnly Date { get; init; }

        /// <summary>
        /// The city in which the passes occurred.
        /// </summary>
        public string City { get; init; } = default!;

        /// <summary>
        /// The total congestion tax fee calculated for the day.
        /// </summary>
        public int TotalFee { get; init; }

    }
}