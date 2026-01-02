namespace AHA.CongestionTax.Application.Mappers
{
    using AHA.CongestionTax.Application.Abstractions.Adapter;
    using AHA.CongestionTax.Application.DTOs;

    public class HolidayRuleDtoToDatesMapper
        : IMapper<HolidayRuleDto, IEnumerable<DateOnly>>
    {
        public static IEnumerable<DateOnly> Map(HolidayRuleDto dto)
        {
            yield return dto.Date;

            if (dto.AppliesToDayBefore)
            {
                yield return dto.Date.AddDays(-1);
            }

        }

    }
}
