namespace AHA.CongestionTax.Application.Adapters
{
    using AHA.CongestionTax.Application.Abstractions.Adapter;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Domain.ValueObjects;

    public class TimeSlotRuleDtoToTimeSlotAdapter
        : ITypeAdapter<TimeSlotRuleDto, TimeSlot>
    {

        public static TimeSlot Adapt(TimeSlotRuleDto dto)
        {
            var start = new TimeOnly(dto.StartHour, dto.StartMinute);
            var end = new TimeOnly(dto.EndHour, dto.EndMinute);

            return new TimeSlot(start, end, dto.Amount);
        }

    }
}
