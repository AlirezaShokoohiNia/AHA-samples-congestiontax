namespace AHA.CongestionTax.Application.DTOs
{

    /// <summary>
    /// Read-side data transfer object representing rule set of a city,
    /// used by CQRS queries and their handlers.
    /// </summary>
    public sealed class RuleSetDto
    {
        /// <summary>
        /// City identifier or name
        /// </summary>
        public string City { get; init; } = default!;

        /// <summary>
        /// All time slot rules for the city
        /// </summary>
        public List<TimeSlotRuleDto> TimeSlots { get; init; } = [];

        /// <summary>
        /// All holiday rules for the city
        /// </summary>
        public List<HolidayRuleDto> Holidays { get; init; } = [];

        /// <summary>
        /// Vehicles that are toll-free in this city
        /// </summary>
        public List<VehicleFreeRuleDto> TollFreeVehicles { get; init; } = [];
    }
}
