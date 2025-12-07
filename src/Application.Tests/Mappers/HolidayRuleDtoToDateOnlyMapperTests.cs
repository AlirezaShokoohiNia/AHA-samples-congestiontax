namespace AHA.CongestionTax.Application.Mappers.Tests
{
    using AHA.CongestionTax.Application.DTOs;
    using Xunit;

    public class HolidayRuleDtoToDateOnlyMapperTests
    {
        [Fact]
        public void Map_ShouldReturnHolidayDate()
        {
            var dto = new HolidayRuleDto
            {
                Date = new DateOnly(2025, 12, 25),
                AppliesToDayBefore = false
            };

            var results = HolidayRuleDtoToDateOnlyMapper.Map(dto).ToList();

            Assert.Single(results);
            Assert.Equal(new DateOnly(2025, 12, 25), results[0]);
        }

        [Fact]
        public void Map_ShouldReturnHolidayAndDayBefore_WhenAppliesToDayBeforeIsTrue()
        {
            var readModel = new HolidayRuleDto
            {
                Date = new DateOnly(2025, 12, 25),
                AppliesToDayBefore = true
            };

            var results = HolidayRuleDtoToDateOnlyMapper.Map(readModel).ToList();

            Assert.Equal(2, results.Count);
            Assert.Contains(new DateOnly(2025, 12, 25), results);
            Assert.Contains(new DateOnly(2025, 12, 24), results);
        }
    }

}
