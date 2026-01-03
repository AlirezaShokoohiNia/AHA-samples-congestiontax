namespace AHA.CongestionTax.Infrastructure.Query.Adapters
{
    using AHA.CongestionTax.Application.Abstractions.Adapter;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Mappers;
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;

    public class RuleSetReadModelToRuleSetDtoAdapter
        : ITypeAdapter<RuleSetReadModel, RuleSetDto>
    {
        public static RuleSetDto Adapt(RuleSetReadModel readModel)
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

    }
}
