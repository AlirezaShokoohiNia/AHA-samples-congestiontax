namespace AHA.CongestionTax.Application.Mappers.Tests
{
    using AHA.CongestionTax.Application.DTOs;
    using Xunit;

    public class HolidayRuleDtoToDatesMapperTests
    {
        [Fact]
        public void Map_Should_IncludeBeforeDay_When_AppliesToDayBefore_Is_True()
        {
            // Arrange
            var dto = new HolidayRuleDto
            {
                Date = new DateOnly(2023, 12, 25),
                AppliesToDayBefore = true
            };

            // Act
            var result = HolidayRuleDtoToDatesMapper.Map(dto);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(new DateOnly(2023, 12, 24), result);
            Assert.Contains(new DateOnly(2023, 12, 25), result);
        }

        [Fact]
        public void Map_Should_NotIncludeBeforeDay_When_AppliesToDayBefore_Is_False()
        {
            // Arrange
            var dto = new HolidayRuleDto
            {
                Date = new DateOnly(2023, 1, 1),
                AppliesToDayBefore = false
            };

            // Act
            var result = HolidayRuleDtoToDatesMapper.Map(dto);

            // Assert
            Assert.Single(result);
            Assert.Contains(new DateOnly(2023, 1, 1), result);
        }
    }

}
