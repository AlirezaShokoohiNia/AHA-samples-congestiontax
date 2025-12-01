namespace AHA.CongestionTax.Application.Mappers.Tests
{
    using AHA.CongestionTax.Application.ReadModels.Queries.RuleSets;
    using Xunit;

    public class HolidayRuleReadModelToDateOnlyMapperTests
    {
        [Fact]
        public void Map_ShouldReturnHolidayDate()
        {
            var readModel = new HolidayRuleReadModel
            {
                Date = new DateOnly(2025, 12, 25),
                AppliesToDayBefore = false
            };

            var results = HolidayRuleReadModelToDateOnlyMapper.Map(readModel).ToList();

            Assert.Single(results);
            Assert.Equal(new DateOnly(2025, 12, 25), results[0]);
        }

        [Fact]
        public void Map_ShouldReturnHolidayAndDayBefore_WhenAppliesToDayBeforeIsTrue()
        {
            var readModel = new HolidayRuleReadModel
            {
                Date = new DateOnly(2025, 12, 25),
                AppliesToDayBefore = true
            };

            var results = HolidayRuleReadModelToDateOnlyMapper.Map(readModel).ToList();

            Assert.Equal(2, results.Count);
            Assert.Contains(new DateOnly(2025, 12, 25), results);
            Assert.Contains(new DateOnly(2025, 12, 24), results);
        }
    }

}
