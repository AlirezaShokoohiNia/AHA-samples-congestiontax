namespace AHA.CongestionTax.Application.Adapters
{
    using AHA.CongestionTax.Application.Abstractions.Adapter;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Domain.ValueObjects;

    public class VehicleFreeRuleDtoToVehicleTypeAdapter
        : ITypeAdapter<VehicleFreeRuleDto, VehicleType>
    {
        public static VehicleType Adapt(VehicleFreeRuleDto dto)
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

    }
}
