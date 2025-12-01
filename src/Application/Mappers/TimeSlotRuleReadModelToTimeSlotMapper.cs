namespace AHA.CongestionTax.Application.Mappers
{
    using AHA.CongestionTax.Application.ReadModels.Queries.RuleSets;
    using AHA.CongestionTax.Domain.ValueObjects;

    /// <summary>
    /// Maps a TimeSlotRuleReadModel into a domain TimeSlot value object.
    /// </summary>
    public static class TimeSlotRuleReadModelToTimeSlotMapper
    {
        /// <summary>
        /// Converts a single TimeSlotRuleReadModel into a TimeSlot VO.
        /// </summary>
        public static TimeSlot Map(TimeSlotRuleReadModel readModel)
        {
            var start = new TimeOnly(readModel.StartHour, readModel.StartMinute);
            var end = new TimeOnly(readModel.EndHour, readModel.EndMinute);

            return new TimeSlot(start, end, readModel.Amount);
        }

        /// <summary>
        /// Converts a collection of TimeSlotRuleReadModels into TimeSlot VOs.
        /// </summary>
        public static IReadOnlyCollection<TimeSlot> MapMany(IEnumerable<TimeSlotRuleReadModel> readModels)
        {
            return [.. readModels.Select(Map)];
        }
    }
}