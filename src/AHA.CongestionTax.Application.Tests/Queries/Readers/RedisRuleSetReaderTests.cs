namespace AHA.CongestionTax.Application.Queries.Readers.Tests
{
    using FluentAssertions;
    using Moq;
    using StackExchange.Redis;
    using Xunit;
    using AHA.CongestionTax.Application.Queries.Readers;

    public class RedisRuleSetReaderTests
    {

        [Fact]
        public async Task GetRuleSetAsync_WhenCityIsProvided_ReturnsRuleSetQueryModel()
        {
            // Arrange
            var city = "Gothenburg";

            var mockDb = new Mock<IDatabase>();
            var json = "{\"City\":\"Gothenburg\",\"TimeSlots\":[],\"Holidays\":[],\"TollFreeVehicles\":[]}";
            mockDb.Setup(db => db.StringGetAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
                  .ReturnsAsync(json);

            var mockRedis = new Mock<IConnectionMultiplexer>();
            mockRedis.Setup(r => r.GetDatabase(It.IsAny<int>(), It.IsAny<object>())).Returns(mockDb.Object);

            var reader = new RedisRuleSetReader(mockRedis.Object); // Will throw at first

            // Act
            var act = async () => await reader.GetRulesForCityAsync(city);

            // Assert
            await act.Should().NotThrowAsync(); // initially fail (red)
        }
    }
}