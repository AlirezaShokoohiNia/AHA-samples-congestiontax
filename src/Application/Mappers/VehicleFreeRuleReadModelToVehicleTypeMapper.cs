namespace AHA.CongestionTax.Application.Mappers
{
    using AHA.CongestionTax.Application.ReadModels.Queries.RuleSets;
    using AHA.CongestionTax.Domain.ValueObjects;

    /// <summary>
    /// Maps VehicleFreeRuleReadModel into VehicleType enum values.
    /// </summary>
    public static class VehicleFreeRuleReadModelToVehicleTypeMapper
    {
        /// <summary>
        /// Converts a single VehicleFreeRuleReadModel into a VehicleType enum.
        /// </summary>
        public static VehicleType Map(VehicleFreeRuleReadModel readModel)
        {
            if (string.IsNullOrWhiteSpace(readModel.VehicleType))
                return VehicleType.Unknown;

            // Normalize input
            var type = readModel.VehicleType.Trim().ToLowerInvariant();

            return type switch
            {
                "car" => VehicleType.Car,
                "motorcycle" => VehicleType.Motorcycle,
                "emergency" => VehicleType.Emergency,
                "diplomat" => VehicleType.Diplomat,
                "military" => VehicleType.Military,
                "foreign" => VehicleType.Foreign,
                _ => VehicleType.Unknown
            };
        }

        /// <summary>
        /// Converts a collection of VehicleFreeRuleReadModels into VehicleType enums.
        /// </summary>
        public static IReadOnlySet<VehicleType> MapMany(IEnumerable<VehicleFreeRuleReadModel> readModels)
        {
            var set = new HashSet<VehicleType>();

            foreach (var rm in readModels)
            {
                _ = set.Add(Map(rm)); // Map(rm) returns VehicleType
            }

            return set;
        }

    }
}
