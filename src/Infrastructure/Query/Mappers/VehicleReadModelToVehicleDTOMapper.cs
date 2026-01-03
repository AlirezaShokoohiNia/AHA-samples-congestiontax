namespace AHA.CongestionTax.Infrastructure.Query.Mappers
{
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;

    /// <summary>
    /// Maps a Vehicle read model into a Vehicle dto.
    /// </summary>
    public class VehicleReadModelToVehicleDTOMapper
        : IReadModelToDtoMapper<VehicleReadModel, VehicleDto>
    {
        /// <summary>
        /// Converts a single Vehicle read model into a Vehicle DTO.
        /// </summary>
        [Obsolete("This mapper is deprecated and will be removed in future versions. using VehicleReadModelToVehicleDTOMapper instead.")]
        public static VehicleDto Map(VehicleReadModel readModel)
        {
            var vehicleTypeCaption = readModel.VehicleType switch
            {
                VehicleTypeReadModel.Car => "Car",
                VehicleTypeReadModel.Motorcycle => "Motorcycle",
                VehicleTypeReadModel.Emergency => "Emergency",
                VehicleTypeReadModel.Diplomat => "Diplomat",
                VehicleTypeReadModel.Military => "Military",
                VehicleTypeReadModel.Foreign => "Foreign",
                _ => "Unknown"
            };


            return new VehicleDto
            {
                VehicleId = readModel.VehicleId,
                LicensePlate = readModel.LicensePlate,
                VehicleType = vehicleTypeCaption
            };
        }

        /// <summary>
        /// Converts a collection of Vehicle read models into Vehicle DTOs.
        /// </summary>
        public static IReadOnlyCollection<VehicleDto> MapMany(IEnumerable<VehicleReadModel> readModels)
            => [.. readModels.Select(Map)];
    }
}