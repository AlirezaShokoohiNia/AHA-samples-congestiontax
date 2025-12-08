namespace AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels
{
    public sealed class TollFreeVehicleRuleReadModel
    {
        /// <summary>
        /// Example: "Emergency", "Motorcycle", "Diplomat", "Foreign", etc.
        /// </summary>
        public string VehicleType { get; init; } = default!;
    }
}