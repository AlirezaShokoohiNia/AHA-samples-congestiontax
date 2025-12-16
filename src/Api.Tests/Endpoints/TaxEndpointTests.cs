namespace AHA.CongestionTax.Api.Endpoints.Tests
{
    using System.Globalization;
    using System.Net;
    using System.Net.Http.Json;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source1;
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class TaxEndpointTests(EndpointTestWebApplicationFactory factory)
        : IClassFixture<EndpointTestWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task GetDailyTaxRecords_Should_ReturnRecords_WhenExists()
        {
            // Arrange: seed DayToll in QueryDbContext
            var queryDb = factory.Services.GetRequiredService<QueryDbContext>();
            queryDb.Vehicles.Add(new VehicleReadModel
            {
                VehicleId = 1,
                LicensePlate = "DAILY123",
                VehicleType = VehicleTypeReadModel.Car
            });
            queryDb.DayTolls.Add(new DayTollReadModel
            {
                VehicleId = 1,
                LicensePlate = "DAILY123",
                City = "Gothenburg",
                Date = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                TotalFee = 20
            });
            queryDb.SaveChanges();

            var from = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var to = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(1));

            // Format as yyyy-MM-dd for query string
            var fromStr = from.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var toStr = to.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            // Act
            var response = await _client.GetAsync($"/vehicles/1/tax-records?from={fromStr}&to={toStr}");
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var records = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<VehicleDailyTaxDto>>();
            Assert.NotNull(records);
            Assert.Single(records);
            Assert.Equal(20, records.First().Tax);
        }

        [Fact]
        public async Task GetDailyTaxRecords_Should_ReturnNotFound_WhenVehicleMissing()
        {
            //Arrange
            var from = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(-1));
            var to = DateOnly.FromDateTime(DateTime.UtcNow.Date.AddDays(1));

            // Format as yyyy-MM-dd for query string
            var fromStr = from.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var toStr = to.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            // Act
            var response = await _client.GetAsync($"/vehicles/999/tax-records?from={fromStr}&to={toStr}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var records = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<VehicleDailyTaxDto>>();
            Assert.NotNull(records);
            Assert.Empty(records);
        }

        [Fact]
        public async Task GetWeeklyTax_Should_ReturnTotal_WhenExists()
        {
            // Arrange: seed vehicle + DayToll
            var queryDb = factory.Services.GetRequiredService<QueryDbContext>();
            queryDb.Vehicles.Add(new VehicleReadModel
            {
                VehicleId = 2,
                LicensePlate = "WEEKLY123",
                VehicleType = VehicleTypeReadModel.Car
            });
            queryDb.DayTolls.Add(new DayTollReadModel
            {
                VehicleId = 2,
                LicensePlate = "WEEKLY123",
                City = "Gothenburg",
                Date = DateOnly.FromDateTime(DateTime.UtcNow.Date),
                TotalFee = 50
            });
            queryDb.SaveChanges();

            // Act
            var response = await _client.GetAsync("/vehicles/2/weekly-tax");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var dto = await response.Content.ReadFromJsonAsync<VehicleTotalTaxDto>();
            Assert.NotNull(dto);
            Assert.Equal("WEEKLY123", dto.LicensePlate);
            Assert.Equal(50, dto.TotalTax);
        }

        [Fact]
        public async Task GetWeeklyTax_Should_ReturnNotFound_WhenVehicleMissing()
        {
            // Act
            var response = await _client.GetAsync("/vehicles/999/weekly-tax");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
