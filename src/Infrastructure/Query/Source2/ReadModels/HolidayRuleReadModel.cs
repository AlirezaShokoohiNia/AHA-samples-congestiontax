namespace AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels
{
    public sealed class HolidayRuleReadModel
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