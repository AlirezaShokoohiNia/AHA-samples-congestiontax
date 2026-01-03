namespace AHA.CongestionTax.Infrastructure.Query.Adapters
{
    using AHA.CongestionTax.Application.Abstractions.Adapter;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;

    public class DayTollReadModelToVehicleDailyTaxDtoAdapter
        : ITypeAdapter<DayTollReadModel, VehicleDailyTaxDto>
    {
        public static VehicleDailyTaxDto Adapt(DayTollReadModel readModel)
        {

            return new VehicleDailyTaxDto
            {
                LicensePlate = readModel.LicensePlate,
                Date = readModel.Date,
                City = readModel.City,
                Tax = readModel.TotalFee
            };
        }

    }
}