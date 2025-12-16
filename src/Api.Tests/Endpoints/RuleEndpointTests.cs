namespace AHA.CongestionTax.Api.Endpoints.Tests
{
    using System.Net;
    using System.Net.Http.Json;
    using AHA.CongestionTax.Application.DTOs;
    using Xunit;

    public class RuleEndpointTests(EndpointTestWebApplicationFactory factory)
        : IClassFixture<EndpointTestWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task GetRules_Should_ReturnRuleSet_WhenFileExists()
        {
            // Arrange: ensure a test file exists in RuleSet:BasePath
            // For example: TestData/gothenburg.rules.json with minimal valid JSON

            // Act
            var response = await _client.GetAsync("/rules/Gothenburg");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var dto = await response.Content.ReadFromJsonAsync<RuleSetDto>();
            Assert.NotNull(dto);
            Assert.Equal("Gothenburg", dto.City);
            Assert.NotEmpty(dto.TimeSlots);
        }

        [Fact]
        public async Task GetRules_Should_ReturnNotFound_WhenFileMissing()
        {
            // Act
            var response = await _client.GetAsync("/rules/UnknownCity");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
