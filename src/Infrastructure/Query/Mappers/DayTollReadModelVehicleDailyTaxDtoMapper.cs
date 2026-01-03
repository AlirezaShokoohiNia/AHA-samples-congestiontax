namespace AHA.CongestionTax.Infrastructure.Query.Mappers
{
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;

    /// <summary>
    /// Maps a DayToll read model into a VehicleDailyTax dto.
    /// </summary>
    [Obsolete("This mapper is deprecated and will be removed in future versions. using DayTollReadModelVehicleDailyTaxDtoAdapter instead.")]
    public class DayTollReadModelVehicleDailyTaxDtoMapper
        : IReadModelToDtoMapper<DayTollReadModel, VehicleDailyTaxDto>
    {
        /// <summary>
        /// Converts a single daytoll read model into a VehicleDailyTax DTO.
        /// </summary>
        public static VehicleDailyTaxDto Map(DayTollReadModel readModel)
        {

            return new VehicleDailyTaxDto
            {
                LicensePlate = readModel.LicensePlate,
                Date = readModel.Date,
                City = readModel.City,
                Tax = readModel.TotalFee
            };
        }

        /// <summary>
        /// Converts a collection of daytoll read models into VehicleDailyTax DTOs.
        /// </summary>
        public static IReadOnlyCollection<VehicleDailyTaxDto> MapMany(IEnumerable<DayTollReadModel> readModels)
            => [.. readModels.Select(Map)];
    }
}