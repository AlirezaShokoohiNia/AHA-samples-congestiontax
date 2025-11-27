namespace AHA.CongestionTax.Domain.DayTollAgg
{
    /// <summary>
    /// Represents an individual toll pass event for a vehicle.
    /// </summary>
    /// <remarks>
    /// Stored as part of the DayToll aggregate. Contains no business logic.
    /// </remarks>
    public class PassRecord : Entity
    {
        /// <summary>
        /// The time when the vehicle passed the toll point.
        /// </summary>
        public TimeOnly Time { get; private set; }

        /// <summary>
        /// Required by EF Core.
        /// </summary>
        private PassRecord() { }

        /// <summary>
        /// Creates a pass record for the specified time.
        /// </summary>
        /// <param name="time">Time of passage.</param>
        public PassRecord(TimeOnly time)
        {
            Time = time;
        }
    }
}
