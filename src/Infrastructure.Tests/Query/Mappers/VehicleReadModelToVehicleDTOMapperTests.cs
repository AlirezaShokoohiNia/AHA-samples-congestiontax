namespace AHA.CongestionTax.Infrastructure.Query.Mappers.Tests
{
    using AHA.CongestionTax.Infrastructure.Query.Mappers;
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;

    public class VehicleReadModelToVehicleDTOMapperTests
    {

        [Fact]
        public void Map_ShouldConvertReadModelToVehicleDTO()
        {
            //Arrange
            var readModel = new VehicleReadModel
            {
                VehicleId = 100,
                LicensePlate = "ABC",
                VehicleType = VehicleTypeReadModel.Car,
            };

            //Act
            var result = VehicleReadModelToVehicleDTOMapper.Map(readModel);

            //Assert
            Assert.Equal(100, result.VehicleId);
            Assert.Equal("ABC", result.LicensePlate);
            Assert.Equal("Car", result.VehicleType);
        }


        [Fact]
        public void MapMany_ShouldConvertMultipleReadModels()
        {
            //Arrange
            var readModels = new[]
           {
            new VehicleReadModel
            {
                VehicleId = 100,
                LicensePlate = "ABC",
                VehicleType = VehicleTypeReadModel.Car,
            },
            new VehicleReadModel
            {
                VehicleId = 101,
                LicensePlate = "DEF",
                VehicleType = VehicleTypeReadModel.Emergency,
            }
            };

            //Act
            var results = VehicleReadModelToVehicleDTOMapper.MapMany(readModels).ToList();

            Assert.Equal(2, results.Count);
            Assert.Equal("ABC", results[0].LicensePlate);
            Assert.Equal("DEF", results[1].LicensePlate);


        }
    }
}