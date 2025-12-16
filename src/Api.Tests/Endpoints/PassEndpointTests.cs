namespace AHA.CongestionTax.Api.Endpoints.Tests
{
    using System.Net;
    using System.Net.Http.Json;
    using AHA.CongestionTax.Application.Commands;
    using Xunit;

    public class PassEndpointTests(EndpointTestWebApplicationFactory factory)
        : IClassFixture<EndpointTestWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task PostPasses_Should_ReturnDailyFee_WhenVehicleExists()
        {
            // Arrange: seed a vehicle so the command can succeed
            var vehicleCommand = new RegisterVehicleCommand(
                LicensePlate: "PASS123",
                VehicleType: "Car"
            );
            var response1 = await _client.PostAsJsonAsync("/vehicles", vehicleCommand);
            Assert.Equal(HttpStatusCode.OK, response1.StatusCode);

            var id = await response1.Content.ReadFromJsonAsync<int>();
            Assert.True(id > 0);

            var passCommand = new RegisterPassCommand(
                LicensePlate: "PASS123",
                Timestamp: DateTime.UtcNow,
                City: "Gothenburg"
            );

            // Act
            var response = await _client.PostAsJsonAsync("/passes", passCommand);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var fee = await response.Content.ReadFromJsonAsync<int>();
            Assert.True(fee >= 0);
        }

        [Fact]
        public async Task PostPasses_Should_ReturnBadRequest_WhenVehicleMissing()
        {
            // Arrange: no vehicle seeded
            var passCommand = new RegisterPassCommand(
                LicensePlate: "NOTFOUND999",
                Timestamp: DateTime.UtcNow,
                City: "Gothenburg"
            );

            // Act
            var response = await _client.PostAsJsonAsync("/passes", passCommand);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
