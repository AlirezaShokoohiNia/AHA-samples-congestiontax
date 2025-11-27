namespace AHA.CongestionTax.Infrastructure.Data.ReadModels.Queries.RuleSets
{

    public sealed class RuleSetReadModel
    {
        /// <summary>
        /// City identifier or name
        /// </summary>
        public string City { get; init; } = default!;

        /// <summary>
        /// All time slot rules for the city
        /// </summary>
        public List<TimeSlotRuleReadModel> TimeSlots { get; init; } = new();

        /// <summary>
        /// All holiday rules for the city
        /// </summary>
        public List<HolidayRuleReadModel> Holidays { get; init; } = new();

        /// <summary>
        /// Vehicles that are toll-free in this city
        /// </summary>
        public List<VehicleFreeRuleReadModel> TollFreeVehicles { get; init; } = new();
    }
}