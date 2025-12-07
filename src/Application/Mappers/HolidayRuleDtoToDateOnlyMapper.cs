namespace AHA.CongestionTax.Application.Mappers
{
    using AHA.CongestionTax.Application.DTOs;

    /// <summary>
    /// Maps HolidayRuleDto into DateOnly values for domain usage.
    /// </summary>
    public static class HolidayRuleDtoToDateOnlyMapper
    {
        /// <summary>
        /// Converts a single HolidayRuleDto into one or two DateOnly values,
        /// depending on AppliesToDayBefore.
        /// </summary>
        public static IEnumerable<DateOnly> Map(HolidayRuleDto dto)
        {
            // Always include the holiday itself
            yield return dto.Date;

            // Optionally include the day before
            if (dto.AppliesToDayBefore)
            {
                yield return dto.Date.AddDays(-1);
            }
        }

        /// <summary>
        /// Converts a collection of HolidayRuleDtos into DateOnly values.
        /// </summary>
        public static IReadOnlySet<DateOnly> MapMany(IEnumerable<HolidayRuleDto> dtos)
        {
            var dates = new HashSet<DateOnly>();

            foreach (var rm in dtos)
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
