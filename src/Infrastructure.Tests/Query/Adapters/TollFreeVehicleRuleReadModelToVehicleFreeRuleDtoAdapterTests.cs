namespace AHA.CongestionTax.Infrastructure.Tests.Query.Adapters
{
    using AHA.CongestionTax.Infrastructure.Query.Adapters;
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;
    using Xunit;

    public class TollFreeVehicleRuleReadModelToVehicleFreeRuleDtoAdapterTests
    {
        [Fact]
        public void Adapt_Should_Work_Correctly()
        {
            // Arrange
            var readModel = new TollFreeVehicleRuleReadModel
            {
                VehicleType = "Car"
            };

            // Act
            var dto = TollFreeVehicleRuleReadModelToVehicleFreeRuleDtoAdapter.Adapt(readModel);

            // Assert
            Assert.Equal(readModel.VehicleType, dto.VehicleType);
        }
    }
}