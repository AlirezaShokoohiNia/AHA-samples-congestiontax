namespace AHA.CongestionTax.Application.Mappers
{
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Domain.ValueObjects;

    /// <summary>
    /// Maps a TimeSlotRule dto into a TimeSlot vo.
    /// </summary>
    public static class TimeSlotRuleDtoToTimeSlotMapper
    {
        /// <summary>
        /// Converts a single TimeSlotRule DTO into a TimeSlot VO.
        /// </summary>
        public static TimeSlot Map(TimeSlotRuleDto dto)
        {
            var start = new TimeOnly(dto.StartHour, dto.StartMinute);
            var end = new TimeOnly(dto.EndHour, dto.EndMinute);

            return new TimeSlot(start, end, dto.Amount);
        }

        /// <summary>
        /// Converts a collection of TimeSlotRule DTO into TimeSlot VOs.
        /// </summary>
        public static IReadOnlyCollection<TimeSlot> MapMany(IEnumerable<TimeSlotRuleDto> dtos)
        {
            return [.. dtos.Select(Map)];
        }
    }
}
