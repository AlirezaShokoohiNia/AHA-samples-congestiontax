namespace AHA.CongestionTax.Infrastructure.Query.Mappers
{
    using System.Collections.Generic;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;

    /// <summary>
    /// Maps a RuleSet read model into a RuleSet dto.
    /// </summary>
    [Obsolete("This mapper is deprecated and will be removed in future versions. using RuleSetReadModelToRuleSetDtoAdapter instead.")]
    public class RuleSetReadModelToRuleSetDto
        : IReadModelToDtoMapper<RuleSetReadModel, RuleSetDto>
    {
        /// <summary>
        /// Converts a single RuleSet read model into a RuleSet DTO.
        /// </summary>
        public static RuleSetDto Map(RuleSetReadModel readModel)
        {
            var timeSlotDtos = TimeSlotRuleReadModelToTimeSlotRuleDto.MapMany(readModel.TimeSlots);
            var holidayDtos = HolidayRuleReadModelToHolidayRuleDtoMapper.MapMany(readModel.Holidays);
            var vehicleFreeDtos = TollFreeVehicleRuleReadModelToVehicleFreeRuleDto.MapMany(readModel.TollFreeVehicles);

            return new RuleSetDto()
            {
                City = readModel.City,
                TimeSlots = [.. timeSlotDtos],
                Holidays = [.. holidayDtos],
                TollFreeVehicles = [.. vehicleFreeDtos]
            };

        }

        /// <summary>
        /// Converts a collection of RuleSet read models into RuleSet DTOs.
        /// </summary>
        public static IReadOnlyCollection<RuleSetDto> MapMany(IEnumerable<RuleSetReadModel> readModels)
            => [.. readModels.Select(Map)];
    }
}
