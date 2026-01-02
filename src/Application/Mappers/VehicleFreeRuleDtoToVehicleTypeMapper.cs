namespace AHA.CongestionTax.Application.Mappers
{
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Domain.ValueObjects;

    /// <summary>
    /// Maps VehicleFreeRuleDto into VehicleType enum values.
    /// </summary>
    [Obsolete("Use VehicleFreeRuleDtoToVehicleTypeAdapter instead.")]
    public static class VehicleFreeRuleDtoToVehicleTypeMapper
    {
        /// <summary>
        /// Converts a single VehicleFreeRuleDto into a VehicleType enum.
        /// </summary>
        public static VehicleType Map(VehicleFreeRuleDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.VehicleType))
                return VehicleType.Unknown;

            // Normalize input
            var type = dto.VehicleType.Trim().ToLowerInvariant();

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
        /// Converts a collection of VehicleFreeRuleDtos into VehicleType enums.
        /// </summary>
        public static IReadOnlySet<VehicleType> MapMany(IEnumerable<VehicleFreeRuleDto> dtos)
        {
            var set = new HashSet<VehicleType>();

            foreach (var rm in dtos)
            {
                _ = set.Add(Map(rm)); // Map(rm) returns VehicleType
            }

            return set;
        }

    }
}
