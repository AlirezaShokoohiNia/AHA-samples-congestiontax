namespace AHA.CongestionTax.Application.DTOs
{
    /// <summary>
    /// Read-side data transfer object representing holiday rule of a rule set,
    /// used by CQRS queries and their handlers.
    /// </summary>
    public sealed class HolidayRuleDto
    {
        /// <summary>
        /// Date of holiday
        /// </summary>
        public DateOnly Date { get; init; }

        /// <summary>
        /// Whether the day before this holiday is toll-free
        /// </summary>
        public bool AppliesToDayBefore { get; init; }
    }
}
