namespace AHA.CongestionTax.Application.Queries.RuleSets
{
    public sealed class HolidayRuleQueryModel
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