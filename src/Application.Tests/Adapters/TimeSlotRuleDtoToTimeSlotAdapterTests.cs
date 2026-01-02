namespace AHA.CongestionTax.Application.Adapters.Tests
{
    using AHA.CongestionTax.Application.DTOs;
    using Xunit;

    public class TimeSlotRuleDtoToTimeSlotAdapterTests
    {
        [Fact]
        public void Adapt_ShouldConvertDtoToTimeSlot()
        {
            var dto = new TimeSlotRuleDto
            {
                StartHour = 7,
                StartMinute = 30,
                EndHour = 9,
                EndMinute = 0,
                Amount = 20
            };

            var result = TimeSlotRuleDtoToTimeSlotAdapter.Adapt(dto);

            Assert.Equal(new TimeOnly(7, 30), result.Start);
            Assert.Equal(new TimeOnly(9, 0), result.End);
            Assert.Equal(20, result.Fee);
        }

    }

}