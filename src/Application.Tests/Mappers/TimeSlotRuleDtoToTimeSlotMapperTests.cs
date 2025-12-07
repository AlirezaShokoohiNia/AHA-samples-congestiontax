namespace AHA.CongestionTax.Application.Mappers.Tests
{
    using AHA.CongestionTax.Application.DTOs;
    using Xunit;

    public class TimeSlotRuleDtoToTimeSlotMapperTests
    {
        [Fact]
        public void Map_ShouldConvertDtoToTimeSlot()
        {
            var dto = new TimeSlotRuleDto
            {
                StartHour = 7,
                StartMinute = 30,
                EndHour = 9,
                EndMinute = 0,
                Amount = 20
            };

            var result = TimeSlotRuleDtoToTimeSlotMapper.Map(dto);

            Assert.Equal(new TimeOnly(7, 30), result.Start);
            Assert.Equal(new TimeOnly(9, 0), result.End);
            Assert.Equal(20, result.Fee);
        }

        [Fact]
        public void MapMany_ShouldConvertMultipleDtos()
        {
            var dtos = new[]
            {
            new TimeSlotRuleDto { StartHour = 6, StartMinute = 0, EndHour = 7, EndMinute = 0, Amount = 10 },
            new TimeSlotRuleDto { StartHour = 7, StartMinute = 0, EndHour = 8, EndMinute = 0, Amount = 15 }
        };

            var results = TimeSlotRuleDtoToTimeSlotMapper.MapMany(dtos).ToList();

            Assert.Equal(2, results.Count);
            Assert.Equal(10, results[0].Fee);
            Assert.Equal(15, results[1].Fee);
        }
    }

}