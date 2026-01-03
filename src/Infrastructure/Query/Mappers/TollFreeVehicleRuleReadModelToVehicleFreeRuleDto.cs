namespace AHA.CongestionTax.Infrastructure.Query.Mappers
{
    using System.Collections.Generic;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;

    /// <summary>
    /// Maps a TollFreeVehicleRule read model into a VehicleFreeRule dto.
    /// </summary>
    [Obsolete("This mapper is deprecated and will be removed in future versions. using TollFreeVehicleRuleReadModelToVehicleFreeRuleDtoAdapter instead.")]
    public class TollFreeVehicleRuleReadModelToVehicleFreeRuleDto
        : IReadModelToDtoMapper<TollFreeVehicleRuleReadModel, VehicleFreeRuleDto>
    {
        /// <summary>
        /// Converts a single TollFreeVehicleRule read model into a TollFreeVehicle DTO.
        /// </summary>
        public static VehicleFreeRuleDto Map(TollFreeVehicleRuleReadModel readModel)
            => new()
            {
                VehicleType = readModel.VehicleType
            };

        /// <summary>
        /// Converts a collection of TollFreeVehicleRule read models into TollFreeVehicle DTOs.
        /// </summary>
        public static IReadOnlyCollection<VehicleFreeRuleDto> MapMany(IEnumerable<TollFreeVehicleRuleReadModel> readModels)
            => [.. readModels.Select(Map)];
    }
}
