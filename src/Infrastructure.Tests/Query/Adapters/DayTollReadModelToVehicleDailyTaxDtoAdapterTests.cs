namespace AHA.CongestionTax.Infrastructure.Query.Adapters.Tests
{
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;
    using Xunit;

    public class DayTollReadModelToVehicleDailyTaxDtoAdapterTests
    {
        [Fact]
        public void Adapt_Should_Work_Correctly()
        {
            // Arrange
            var readModel = new DayTollReadModel
            {
                LicensePlate = "XYZ123",
                Date = new DateOnly(2024, 12, 25),
                City = "TestCity",
                TotalFee = 150
            };

            // Act
            var dto = DayTollReadModelToVehicleDailyTaxDtoAdapter.Adapt(readModel);

            // Assert
            Assert.Equal("XYZ123", dto.LicensePlate);
            Assert.Equal(new DateOnly(2024, 12, 25), dto.Date);
            Assert.Equal("TestCity", dto.City);
            Assert.Equal(150, dto.Tax);
        }
    }
}