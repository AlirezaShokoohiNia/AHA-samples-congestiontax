namespace AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels
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
        public List<TollFreeVehicleRuleReadModel> TollFreeVehicles { get; init; } = new();
    }
}