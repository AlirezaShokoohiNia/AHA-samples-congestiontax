namespace AHA.CongestionTax.Application.ReadModels.Queries.RuleSets
{
    public sealed class VehicleFreeRuleReadModel
    {
        /// <summary>
        /// Example: "Emergency", "Motorcycle", "Diplomat", "Foreign", etc.
        /// </summary>
        public string VehicleType { get; init; } = default!;
    }
}