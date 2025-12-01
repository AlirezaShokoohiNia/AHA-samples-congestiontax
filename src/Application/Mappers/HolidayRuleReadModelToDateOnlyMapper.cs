namespace AHA.CongestionTax.Application.Mappers
{
    using AHA.CongestionTax.Application.Abstractions;
    using AHA.CongestionTax.Application.ReadModels.Queries.RuleSets;

    /// <summary>
    /// Maps HolidayRuleReadModel into DateOnly values for domain usage.
    /// </summary>
    public static class HolidayRuleReadModelToDateOnlyMapper
    {
        /// <summary>
        /// Converts a single HolidayRuleReadModel into one or two DateOnly values,
        /// depending on AppliesToDayBefore.
        /// </summary>
        public static IEnumerable<DateOnly> Map(HolidayRuleReadModel readModel)
        {
            // Always include the holiday itself
            yield return readModel.Date;

            // Optionally include the day before
            if (readModel.AppliesToDayBefore)
            {
                yield return readModel.Date.AddDays(-1);
            }
        }

        /// <summary>
        /// Converts a collection of HolidayRuleReadModels into DateOnly values.
        /// </summary>
        public static IReadOnlySet<DateOnly> MapMany(IEnumerable<HolidayRuleReadModel> readModels)
        {
            var dates = new HashSet<DateOnly>();

            foreach (var rm in readModels)
            {
                foreach (var date in Map(rm)) // Map(rm) returns IEnumerable<DateOnly>
                {
                    _ = dates.Add(date); // HashSet ensures uniqueness
                }
            }

            return dates;
        }
    }
}
