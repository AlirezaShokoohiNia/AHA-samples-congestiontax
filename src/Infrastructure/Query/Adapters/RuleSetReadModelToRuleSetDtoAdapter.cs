namespace AHA.CongestionTax.Infrastructure.Query.Adapters
{
    using AHA.CongestionTax.Application.Abstractions.Adapter;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;

    /// <summary>
    /// Adapter to convert RuleSetReadModel to RuleSetDto
    /// </summary>
    public class RuleSetReadModelToRuleSetDtoAdapter
        : ITypeAdapter<RuleSetReadModel, RuleSetDto>
    {
        public static RuleSetDto Adapt(RuleSetReadModel readModel)
        {
            var timeSlotDtos = MappingHelper.MapEach(
                    readModel.TimeSlots,
                    TimeSlotRuleReadModelToTimeSlotRuleDtoAdapter.Adapt);

            var holidayDtos = MappingHelper.MapEach(
                readModel.Holidays,
                HolidayRuleReadModelToHolidayRuleDtoAdapter.Adapt);

            var vehicleFreeDtos = MappingHelper.MapEach(
                readModel.TollFreeVehicles,
                TollFreeVehicleRuleReadModelToVehicleFreeRuleDtoAdapter.Adapt);

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
