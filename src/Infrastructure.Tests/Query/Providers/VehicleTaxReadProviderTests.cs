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
            using var queryContext = SqliteInMemoryQueryDbContextFactory.CreateContext();

            // Seed DayToll read models
            queryContext.SetData(new List<DayTollReadModel>
                {
                    new ()
                    {
                        VehicleId = 1,
                        LicensePlate="ABC",
                        Date = DateOnly.FromDateTime(DateTime.UtcNow),
                        City="TestCity",
                        TotalFee = 50
                    },
                    new ()
                    {
                        VehicleId = 1,
                        LicensePlate="ABC",
                        Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)),
                        City="TestCity",
                        TotalFee = 30
                    }
                });

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
            using var queryContext = SqliteInMemoryQueryDbContextFactory.CreateContext();

            // No DayToll records seeded
            queryContext.SetData(new List<DayTollReadModel>());

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
            using var queryContext = SqliteInMemoryQueryDbContextFactory.CreateContext();

            // No Vehicle records seeded
            queryContext.SetData(new List<VehicleReadModel>());

            var provider = new VehicleTaxReadProvider(queryContext);

            var result = await provider.GetWeeklyTotalTaxAsync(99, CancellationToken.None);

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public async Task GetWeeklyTotalTaxAsync_ReturnsZeroWhenNoTolls()
        {
            using var queryContext = SqliteInMemoryQueryDbContextFactory.CreateContext();

            // Seed vehicle but no tolls
            queryContext.SetData(new List<VehicleReadModel>
        {
            new () { VehicleId = 1, LicensePlate = "ABC123" }
        });

            var provider = new VehicleTaxReadProvider(queryContext);

            var result = await provider.GetWeeklyTotalTaxAsync(1, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(0, result.Value!.TotalTax);
            Assert.Equal("ABC123", result.Value.LicensePlate);
        }

        [Fact]
        public async Task GetWeeklyTotalTaxAsync_ReturnsSumWhenTollsExist()
        {
            using var queryContext = SqliteInMemoryQueryDbContextFactory.CreateContext();

            // Seed vehicle and tolls
            queryContext.SetData(new List<VehicleReadModel>
        {
            new () { VehicleId = 1, LicensePlate = "XYZ789" }
        });

            queryContext.SetData(new List<DayTollReadModel>
        {
            new ()
            {
                VehicleId = 1,
                LicensePlate="XYZ789",
                Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-3)),
                City="TestCity",
                TotalFee = 40
            },
            new ()
            {
                VehicleId = 1,
                LicensePlate="XYZ789",
                Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-2)),
                City="TestCity",
                TotalFee = 60
            }
        });

            var provider = new VehicleTaxReadProvider(queryContext);

            var result = await provider.GetWeeklyTotalTaxAsync(1, CancellationToken.None);

            Assert.True(result.IsSuccess);
            Assert.Equal(100, result.Value!.TotalTax);
            Assert.Equal("XYZ789", result.Value.LicensePlate);
        }
    }
}