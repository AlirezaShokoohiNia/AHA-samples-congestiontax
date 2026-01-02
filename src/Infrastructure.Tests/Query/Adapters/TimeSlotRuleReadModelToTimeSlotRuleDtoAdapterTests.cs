namespace AHA.CongestionTax.Infrastructure.Query.Adapters.Tests
{
    using AHA.CongestionTax.Infrastructure.Query.Adapters;
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;
    using Xunit;

    public class TimeSlotRuleReadModelToTimeSlotRuleDtoAdapterTests
    {
        [Fact]
        public void Adapt_Should_Work_Correctly()
        {
            // Arrange
            var readModel = new TimeSlotRuleReadModel
            {
                StartHour = 8,
                StartMinute = 0,
                EndHour = 9,
                EndMinute = 30,
                Amount = 15
            };

            // Act
            var dto = TimeSlotRuleReadModelToTimeSlotRuleDtoAdapter.Adapt(readModel);

            // Assert
            Assert.Equal(readModel.StartHour, dto.StartHour);
            Assert.Equal(readModel.StartMinute, dto.StartMinute);
            Assert.Equal(readModel.EndHour, dto.EndHour);
            Assert.Equal(readModel.EndMinute, dto.EndMinute);
            Assert.Equal(readModel.Amount, dto.Amount);
        }
    }
}