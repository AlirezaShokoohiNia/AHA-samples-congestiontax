namespace AHA.CongestionTax.Domain.ValueObjects
{
    /// <summary>
    /// Represents a toll rule time slot with start/end time and fee.
    /// </summary>
    public readonly record struct TimeSlot(
        TimeOnly Start,
        TimeOnly End,
        int Fee
    )
    {
        /// <summary>
        /// Checks if a given time is inside the slot.
        /// </summary>
        public bool Contains(TimeOnly time) =>
            time >= Start && time <= End;
    }
}
