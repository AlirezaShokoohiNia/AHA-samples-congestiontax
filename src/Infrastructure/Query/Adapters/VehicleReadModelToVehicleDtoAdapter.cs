namespace AHA.CongestionTax.Infrastructure.Query.Adapters
{
    using AHA.CongestionTax.Application.Abstractions.Adapter;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;

    public class VehicleReadModelToVehicleDtoAdapter
        : ITypeAdapter<VehicleReadModel, VehicleDto>
    {
        public static VehicleDto Adapt(VehicleReadModel readModel)
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

    }
}