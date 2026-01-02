namespace AHA.CongestionTax.Application.Mappers
{
    using AHA.CongestionTax.Application.Abstractions.Adapter;
    using AHA.CongestionTax.Application.DTOs;

    /// <summary>
    /// Maps HolidayRuleDto into DateOnly values for domain usage.
    /// </summary>
    public class HolidayRuleDtoToDatesMapper
        : IMapper<HolidayRuleDto, IEnumerable<DateOnly>>
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

    }
}
