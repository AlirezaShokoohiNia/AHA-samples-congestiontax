namespace AHA.CongestionTax.Infrastructure.Query.Mappers
{
    using System.Collections.Generic;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;

    /// <summary>
    /// Maps a TimeSlotRule read model into a TimeSlotRule dto.
    /// </summary>
    public class TimeSlotRuleReadModelToTimeSlotRuleDto
        : IReadModelToDtoMapper<TimeSlotRuleReadModel, TimeSlotRuleDto>
    {
        /// <summary>
        /// Converts a single TimeSlotRule read model into a TimeSlotRule DTO.
        /// </summary>
        public static TimeSlotRuleDto Map(TimeSlotRuleReadModel readModel)
            => new()
            {
                StartHour = readModel.StartHour,
                StartMinute = readModel.StartMinute,
                EndHour = readModel.EndHour,
                EndMinute = readModel.EndMinute,
                Amount = readModel.Amount
            };

        /// <summary>
        /// Converts a collection of TimeSlotRule read models into TimeSlotRule DTOs.
        /// </summary>
        public static IReadOnlyCollection<TimeSlotRuleDto> MapMany(IEnumerable<TimeSlotRuleReadModel> readModels)
            => [.. readModels.Select(Map)];

    }
}
