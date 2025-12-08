namespace AHA.CongestionTax.Infrastructure.Query.Providers.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Xunit;
    using FluentAssertions;

    public class RuleSetReadFileProviderTests
    {
        [Fact]
        public async Task GetRulesForCityAsync_WhenFileExists_ReturnsRuleSet()
        {
            // Arrange
            var basePath = Path.Combine(AppContext.BaseDirectory, "TestData");
            Directory.CreateDirectory(basePath);

            var json = /*lang=json,strict*/ """{ "City": "Gothenburg", "TimeSlots": [], "Holidays": [], "TollFreeVehicles": [] }""";
            File.WriteAllText(Path.Combine(basePath, "gothenburg.rules.json"), json);

            var reader = new RuleSetReadFileProvider(basePath);

            // Act
            var result = await reader.GetRulesForCityAsync("Gothenburg");

            // Assert
            result.IsSuccess.Should().BeTrue(result.Error);
            result.Value!.City.Should().Be("Gothenburg");
        }

        [Fact]
        public async Task GetRulesForCityAsync_WhenFileMissing_ReturnsFailure()
        {
            // Arrange
            var basePath = Path.Combine(AppContext.BaseDirectory, "TestData");
            Directory.CreateDirectory(basePath);

            var reader = new RuleSetReadFileProvider(basePath);

            // Act
            var result = await reader.GetRulesForCityAsync("NonExistingCity");

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("not found");
        }

        [Fact]
        public async Task GetRulesForCityAsync_WhenJsonInvalid_ReturnsFailure()
        {
            // Arrange
            var basePath = Path.Combine(AppContext.BaseDirectory, "TestData");
            Directory.CreateDirectory(basePath);

            var invalidJson = "this is not valid json!";
            File.WriteAllText(Path.Combine(basePath, "malmo.rules.json"), invalidJson);

            var reader = new RuleSetReadFileProvider(basePath);

            // Act
            var result = await reader.GetRulesForCityAsync("Malmo");

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("Invalid JSON format");
        }
    }

}