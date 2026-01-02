namespace AHA.CongestionTax.Infrastructure.Tests.Query.Adapters
{
    using AHA.CongestionTax.Infrastructure.Query.Adapters;
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;
    using Xunit;

    public class VehicleReadModelToVehicleDtoAdapterTests
    {
        [Fact]
        public void Adapt_Should_Work_Correctly()
        {
            // Arrange
            var readModel = new VehicleReadModel
            {
                VehicleId = 123,
                LicensePlate = "ABC-123",
                VehicleType = VehicleTypeReadModel.Car
            };

            // Act
            var dto = VehicleReadModelToVehicleDtoAdapter.Adapt(readModel);

            // Assert
            Assert.Equal(readModel.VehicleId, dto.VehicleId);
            Assert.Equal(readModel.LicensePlate, dto.LicensePlate);
            Assert.Equal("Car", dto.VehicleType);
        }
    }
}