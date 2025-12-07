namespace AHA.CongestionTax.Application.DTOs
{
    /// <summary>
    /// Read-side data transfer object representing vehicle free rule of a rule set,
    /// used by CQRS queries and their handlers.
    /// </summary>
    public sealed class VehicleFreeRuleDto
    {
        /// <summary>
        /// Example: "Emergency", "Motorcycle", "Diplomat", "Foreign", etc.
        /// </summary>
        public string VehicleType { get; init; } = default!;
    }
}
