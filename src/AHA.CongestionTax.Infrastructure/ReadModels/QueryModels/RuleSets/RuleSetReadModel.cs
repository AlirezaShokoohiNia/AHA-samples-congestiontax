namespace AHA.CongestionTax.Application.Queries.RuleSets
{
    public sealed class RuleSetQueryModel
    {
        /// <summary>
        /// City identifier or name
        /// </summary>
        public string City { get; init; } = default!;

        /// <summary>
        /// All time slot rules for the city
        /// </summary>
        public List<TimeSlotRuleQueryModel> TimeSlots { get; init; } = new();

        /// <summary>
        /// All holiday rules for the city
        /// </summary>
        public List<HolidayRuleQueryModel> Holidays { get; init; } = new();

        /// <summary>
        /// Vehicles that are toll-free in this city
        /// </summary>
        public List<VehicleFreeRuleQueryModel> TollFreeVehicles { get; init; } = new();
    }
}