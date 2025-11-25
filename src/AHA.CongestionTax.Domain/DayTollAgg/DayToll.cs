namespace AHA.CongestionTax.Domain.DayTollAgg
{
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.VehicleAgg;

    /// <summary>
    /// Aggregate root representing a vehicle’s congestion tax usage for a single day.
    /// </summary>
    /// <remarks>
    /// Tracks all pass records within a specific city and date, and stores
    /// the final calculated daily fee. Fee calculation itself is delegated
    /// to domain services.
    /// </remarks>
    public class DayToll : AggregateRoot
    {
        /// <summary>
        /// The vehicle associated with this day's toll evaluation.
        /// </summary>
        public Vehicle Vehicle { get; private set; } = default!;

        /// <summary>
        /// The city in which the passes occurred.
        /// </summary>
        public string City { get; private set; } = default!;

        /// <summary>
        /// The calendar date for which tolls are being aggregated.
        /// </summary>
        public DateOnly Date { get; private set; }

        /// <summary>
        /// The total congestion tax fee calculated for the day.
        /// </summary>
        public int TotalFee { get; private set; }

        private readonly List<PassRecord> _passes = [];

        /// <summary>
        /// All recorded passes for the vehicle on this day.
        /// </summary>
        public IReadOnlyCollection<PassRecord> Passes => _passes;

        /// <summary>
        /// Required by EF Core.
        /// </summary>
        private DayToll() { }

        /// <summary>
        /// Creates a new daily toll aggregate for a vehicle in a given city and date.
        /// </summary>
        public DayToll(Vehicle vehicle, string city, DateOnly date)
        {
            Vehicle = vehicle;
            City = city;
            Date = date;
        }

        /// <summary>
        /// Adds a recorded pass time to the day’s toll history.
        /// </summary>
        /// <param name="time">The time the vehicle passed a toll point.</param>
        public void AddPass(TimeOnly time) =>
            _passes.Add(new PassRecord(time));

        /// <summary>
        /// Applies the final calculated fee for the day.
        /// </summary>
        /// <param name="dailyFee">The computed daily fee.</param>
        /// <exception cref="DomainException">Thrown when fee is negative.</exception>
        public void ApplyCalculatedFee(int dailyFee)
        {
            if (dailyFee < 0)
                throw new DomainException("Fee cannot be negative.");

            TotalFee = dailyFee;
        }
    }
}
