namespace AHA.CongestionTax.Infrastructure.Query.Adapters
{
    using AHA.CongestionTax.Application.Abstractions.Adapter;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;

    public class TollFreeVehicleRuleReadModelToVehicleFreeRuleDtoAdapter
        : ITypeAdapter<TollFreeVehicleRuleReadModel, VehicleFreeRuleDto>
    {
        public static VehicleFreeRuleDto Adapt(TollFreeVehicleRuleReadModel readModel)
            => new()
            {
                VehicleType = readModel.VehicleType
            };

    }
}
