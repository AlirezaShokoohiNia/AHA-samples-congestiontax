namespace AHA.CongestionTax.Infrastructure.Query.Providers.Tests
{
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;
    using AHA.CongestionTax.Infrastructure.Query.Source1.Tests;

    public class VehicleTaxReadProviderTests
    {
        [Fact]
        public async Task GetDailyTaxRecordsAsync_ReturnsRecordsWithinDateRange()
        {
            //Arrange
            using var queryContext = QueryDbContextTestFactory.CreateContext();

            // Seed DayToll read models
            queryContext.DayTolls.AddRange(
                    new()
                    {
                        VehicleId = 1,
                        LicensePlate = "ABC",
                        Date = DateOnly.FromDateTime(DateTime.UtcNow),
                        City = "TestCity",
                        TotalFee = 50
                    },
                    new()
                    {
                        VehicleId = 1,
                        LicensePlate = "ABC",
                        Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)),
                        City = "TestCity",
                        TotalFee = 30
                    }
                            );
            queryContext.SaveChanges();
            var provider = new VehicleTaxReadProvider(queryContext);

            //Act
            var result = await provider.GetDailyTaxRecordsAsync(
                1,
                DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-2)),
                DateOnly.FromDateTime(DateTime.UtcNow),
                CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess, result.Error);
            Assert.Equal(2, result.Value!.Count);
        }

        [Fact]
        public async Task GetDailyTaxRecordsAsync_ReturnsEmptyWhenNoRecords()
        {
            //Arrange
            using var queryContext = QueryDbContextTestFactory.CreateContext();

            var provider = new VehicleTaxReadProvider(queryContext);

            //Act
            var result = await provider.GetDailyTaxRecordsAsync(
                1,
                DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-2)),
                DateOnly.FromDateTime(DateTime.UtcNow),
                CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Empty(result.Value!);
        }

        [Fact]
        public async Task GetWeeklyTotalTaxAsync_ReturnsFailureWhenVehicleNotFound()
        {
            //Arrange
            using var queryContext = QueryDbContextTestFactory.CreateContext();

            var provider = new VehicleTaxReadProvider(queryContext);

            //Act
            var result = await provider.GetWeeklyTotalTaxAsync(99, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetWeeklyTotalTaxAsync_ReturnsZeroWhenNoTolls()
        {
            //Arrange
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

            var provider = new VehicleTaxReadProvider(queryContext);

            //Act
            var result = await provider.GetWeeklyTotalTaxAsync(1, CancellationToken.None);

            //Assert    
            Assert.True(result.IsSuccess);
            Assert.Equal(0, result.Value!.TotalTax);
            Assert.Equal("ABC123", result.Value.LicensePlate);
        }

        [Fact]
        public async Task GetWeeklyTotalTaxAsync_ReturnsSumWhenTollsExist()
        {
            //Arrange
            using var queryContext = QueryDbContextTestFactory.CreateContext();

            queryContext.Vehicles.Add(
                                new VehicleReadModel
                                {
                                    VehicleId = 1,
                                    LicensePlate = "XYZ789",
                                    VehicleType = VehicleTypeReadModel.Car
                                }
                            );

            // Seed DayToll read models
            queryContext.DayTolls.AddRange(
                    new()
                    {
                        VehicleId = 1,
                        LicensePlate = "XYZ789",
                        Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-3)),
                        City = "TestCity",
                        TotalFee = 40
                    },
                    new()
                    {
                        VehicleId = 1,
                        LicensePlate = "XYZ789",
                        Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-2)),
                        City = "TestCity",
                        TotalFee = 60
                    }
                            );

            queryContext.SaveChanges();

            var provider = new VehicleTaxReadProvider(queryContext);

            //Act
            var result = await provider.GetWeeklyTotalTaxAsync(1, CancellationToken.None);

            //Assert    
            Assert.True(result.IsSuccess);
            Assert.Equal(100, result.Value!.TotalTax);
            Assert.Equal("XYZ789", result.Value.LicensePlate);
        }
    }
}