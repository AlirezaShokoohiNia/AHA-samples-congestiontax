namespace AHA.CongestionTax.Application.Queries.Readers
{
    using FluentAssertions;

    public class FileRuleSetReaderTests
    {
        [Fact]
        public async Task GetRulesForCityAsync_WhenFileExists_ReturnsRuleSet()
        {
            // Arrange
            var basePath = Path.Combine(AppContext.BaseDirectory, "TestData");
            Directory.CreateDirectory(basePath);

            var json = /*lang=json,strict*/ """{ "City": "Gothenburg", "TimeSlots": [], "Holidays": [], "TollFreeVehicles": [] }""";

            File.WriteAllText(Path.Combine(basePath, "gothenburg.rules.json"), json);

            var reader = new FileRuleSetReader(basePath);

            // Act
            var result = await reader.GetRulesForCityAsync("Gothenburg");

            // Assert
            result.Should().NotBeNull();
            result!.City.Should().Be("Gothenburg");
        }

        [Fact]
        public async Task GetRulesForCityAsync_WhenKeyMissing_ReturnsNull()
        {
            // Arrange
            var basePath = Path.Combine(AppContext.BaseDirectory, "TestData");
            Directory.CreateDirectory(basePath);

            var reader = new FileRuleSetReader(basePath);

            // Act
            var result = await reader.GetRulesForCityAsync("NonExistingCity");

            // Assert
            result.Should().BeNull();
        }
    }
}