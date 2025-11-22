namespace AHA.CongestionTax.Application.Queries.RuleSets
{
    public sealed class VehicleFreeRuleQueryModel
    {
        /// <summary>
        /// Example: "Emergency", "Motorcycle", "Diplomat", "Foreign", etc.
        /// </summary>
        public string VehicleType { get; init; } = default!;
    }
}