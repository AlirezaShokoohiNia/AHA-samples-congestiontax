namespace AHA.CongestionTax.Infrastructure.Query.Adapters.Tests
{
    using AHA.CongestionTax.Infrastructure.Query.Source2.ReadModels;

    public class RuleSetReadModelToRuleSetDtoAdapterTests
    {
        [Fact]
        public void Adapt_Should_Work_Correctly()
        {
            // Arrange
            var readModel = new RuleSetReadModel
            {
                City = "TestCity",
                TimeSlots = [
                    new TimeSlotRuleReadModel
                        {
                            StartHour = 8,
                            StartMinute = 0,
                            EndHour = 9,
                            EndMinute = 30,
                            Amount = 15
                        }
                ],
                Holidays = [
                    new HolidayRuleReadModel
                        {
                            AppliesToDayBefore = true,
                            Date = new DateOnly(2024, 12, 25),
                        }
                ],
                TollFreeVehicles = [
                    new TollFreeVehicleRuleReadModel
                        {
                            VehicleType = "Car"
                        }
                ]
            };

            // Act
            var dto = RuleSetReadModelToRuleSetDtoAdapter.Adapt(readModel);

            // Assert
            Assert.Equal(1, dto.TimeSlots.Count);
            Assert.Equal(8, dto.TimeSlots[0].StartHour);
            Assert.Equal(0, dto.TimeSlots[0].StartMinute);
            Assert.Equal(9, dto.TimeSlots[0].EndHour);
            Assert.Equal(30, dto.TimeSlots[0].EndMinute);
            Assert.Equal(15, dto.TimeSlots[0].Amount);
            Assert.Equal(1, dto.Holidays.Count);
            Assert.Equal(true, dto.Holidays[0].AppliesToDayBefore);
            Assert.Equal(new DateOnly(2024, 12, 25), dto.Holidays[0].Date);
            Assert.Equal(1, dto.TollFreeVehicles.Count);
            Assert.Equal("Car", dto.TollFreeVehicles[0].VehicleType);
        }
    }
}