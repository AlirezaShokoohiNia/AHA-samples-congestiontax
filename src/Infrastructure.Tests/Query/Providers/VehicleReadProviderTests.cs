namespace AHA.CongestionTax.Infrastructure.Query.Providers.Tests
{
    using System.Threading.Tasks;
    using AHA.CongestionTax.Infrastructure.Query.Providers;
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;
    using AHA.CongestionTax.Infrastructure.Query.Source1.Tests;
    using Xunit;

    public class VehicleReadProviderTests
    {
        [Fact]
        public async Task GetVehicleAsync_ShouldReturnFailure_WhenLicensePlateIsNullOrEmpty()
        {
            //Arrage
            using var queryContext = QueryDbContextTestFactory.CreateContext();
            var provider = new VehicleReadProvider(queryContext);

            // Act
            var result1 = await provider.GetVehicleAsync(null!);
            var result2 = await provider.GetVehicleAsync("");

            // Assert
            Assert.False(result1.IsSuccess);
            Assert.Equal("invalid-platenumber", result1.Error);

            Assert.False(result2.IsSuccess);
            Assert.Equal("invalid-platenumber", result2.Error);
        }

        [Fact]
        public async Task GetVehicleAsync_ShouldReturnFailure_WhenVehicleNotFound()
        {
            //Arrange
            using var queryContext = QueryDbContextTestFactory.CreateContext();
            var provider = new VehicleReadProvider(queryContext);

            // Act
            var result = await provider.GetVehicleAsync("ABC123");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("not found", result.Error);
        }

        [Fact]
        public async Task GetVehicleAsync_ShouldReturnSuccess_WhenVehicleExists()
        {
            // Arrange
            using var queryContext = QueryDbContextTestFactory.CreateContext();
            queryContext.Vehicles.Add(
                                new VehicleReadModel
                                {
                                    VehicleId = 1,
                                    LicensePlate = "ABC123",
                                    VehicleType = VehicleTypeReadModel.Car
                                }
                            );
            queryContext.SaveChanges();
            var provider = new VehicleReadProvider(queryContext);

            // Act
            var result = await provider.GetVehicleAsync("ABC123");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal("ABC123", result.Value.LicensePlate);
        }

    }
}
