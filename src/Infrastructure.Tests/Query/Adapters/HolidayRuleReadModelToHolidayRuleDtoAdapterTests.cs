namespace AHA.CongestionTax.Infrastructure.Query.Adapters.Tests
{
    using AHA.CongestionTax.Infrastructure.Query.Adapters;
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;
    using Xunit;

    public class HolidayRuleReadModelToHolidayRuleDtoAdapterTests
    {

        [Fact]
        public void Adapt_Should_Work_Correctly()
        {
            // Arrange
            var readModel = new HolidayRuleReadModel
            {
                AppliesToDayBefore = true,
                Date = new DateOnly(2024, 12, 25),
            };

            // Act
            var dto = HolidayRuleReadModelToHolidayRuleDtoAdapter.Adapt(readModel);

            // Assert
            Assert.Equal(true, dto.AppliesToDayBefore);
            Assert.Equal(new DateOnly(2024, 12, 25), dto.Date);
        }
    }
}