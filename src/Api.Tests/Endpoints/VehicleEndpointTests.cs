namespace AHA.CongestionTax.Api.Endpoints.Tests
{
    using System.Net;
    using System.Net.Http.Json;
    using AHA.CongestionTax.Application.Commands;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Infrastructure.Query.Source1;
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;
    using Microsoft.Extensions.DependencyInjection;

    public class VehicleEndpointTests(EndpointTestWebApplicationFactory factory)
                : IClassFixture<EndpointTestWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task PostVehicles_Should_ReturnVehicleId()
        {
            //Arrange
            var command = new RegisterVehicleCommand(
                LicensePlate: "TEST123",
                VehicleType: "Car"
            );

            //Act
            var response = await _client.PostAsJsonAsync("/vehicles", command);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var id = await response.Content.ReadFromJsonAsync<int>();
            Assert.True(id > 0);
        }

        [Fact]
        public async Task GetVehicle_Should_ReturnVehicleDto_WhenExists()
        {
            //Arrange
            var readDb = factory.Services.GetRequiredService<QueryDbContext>();
            readDb.Vehicles.Add(
                                new VehicleReadModel
                                {
                                    VehicleId = 1,
                                    LicensePlate = "EXIST456",
                                    VehicleType = VehicleTypeReadModel.Car
                                }
                            );
            readDb.SaveChanges();
            var command = new RegisterVehicleCommand
            (
                LicensePlate: "EXIST456",
                VehicleType: "Car"
            );

            //Act
            var response = await _client.GetAsync($"/vehicles/{command.LicensePlate}");

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var dto = await response.Content.ReadFromJsonAsync<VehicleDto>();
            Assert.NotNull(dto);
            Assert.Equal(command.LicensePlate, dto.LicensePlate);
        }

        [Fact]
        public async Task GetVehicle_Should_ReturnNotFound_WhenMissing()
        {
            //Act
            var response = await _client.GetAsync("/vehicles/NOTFOUND999");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }


}