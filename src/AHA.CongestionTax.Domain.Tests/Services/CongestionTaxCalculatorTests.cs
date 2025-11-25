namespace AHA.CongestionTax.Domain.Services.timeSlots
{
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.ValueObjects;
    using AHA.CongestionTax.Domain.VehicleAgg;

    public class CongestionTaxCalculatorTests
    {
        [Fact]
        public void TollFreeVehicle_ShouldReturnZeroFee()
        {
            //Arrange
            var calc = new CongestionTaxCalculator();
            var vehicle = new Vehicle("EMR001", VehicleType.Emergency);
            var dayToll = new DayToll(vehicle, "Gothenburg", new DateOnly(2025, 11, 25));
            dayToll.AddPass(new TimeOnly(8, 30));

            //Act
            var result = calc.CalculateDailyFee(
                dayToll,
                [],
                new HashSet<DateOnly>(),
                new HashSet<VehicleType> { VehicleType.Emergency });

            //Assert
            Assert.Equal(0, result.TotalFee);
        }

        [Fact]
        public void WeekendOrHoliday_ShouldReturnZeroFee()
        {
            //Arrange
            var calc = new CongestionTaxCalculator();
            var vehicle = new Vehicle("ABC123", VehicleType.Car);
            var saturday = new DateOnly(2025, 11, 29); // Saturday
            var dayToll = new DayToll(vehicle, "Gothenburg", saturday);
            dayToll.AddPass(new TimeOnly(9, 0));

            //Act
            var result = calc.CalculateDailyFee(
                dayToll,
                [],
                new HashSet<DateOnly> { saturday },
                new HashSet<VehicleType>());

            //Assert
            Assert.Equal(0, result.TotalFee);
        }

    }
}