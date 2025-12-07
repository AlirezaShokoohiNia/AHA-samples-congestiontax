namespace AHA.CongestionTax.Application.DTOs
{
    /// <summary>
    /// Read-side data transfer object representing timeslot rule of a rule set,
    /// used by CQRS queries and their handlers.
    /// </summary>
    public sealed class TimeSlotRuleDto
    {
        /// <summary>
        /// Start hour of the time slot (0–23)
        /// </summary>
        public int StartHour { get; init; }

        /// <summary>
        /// Start minute of the time slot (0–59)
        /// </summary>
        public int StartMinute { get; init; }

        /// <summary>
        /// End hour of the time slot (0–23)
        /// </summary>
        public int EndHour { get; init; }

        /// <summary>
        /// End minute of the time slot (0–59)
        /// </summary>
        public int EndMinute { get; init; }

        /// <summary>
        /// Toll amount in SEK
        /// </summary>
        public int Amount { get; init; }

    }
}
