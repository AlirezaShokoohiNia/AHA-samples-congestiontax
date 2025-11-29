namespace AHA.CongestionTax.Domain.Services.Tests
{
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.ValueObjects;
    using AHA.CongestionTax.Domain.VehicleAgg;

    public class CongestionTaxCalculatorTests
    {
        [Fact]
        public void TollFreeVehicle_ShouldReturnZeroFee()
        {
            // Arrange
            var calc = new CongestionTaxCalculator();
            var vehicle = new Vehicle("EMR001", VehicleType.Emergency);
            var dayToll = new DayToll(vehicle, "Gothenburg", new DateOnly(2025, 11, 25));
            dayToll.AddPass(new TimeOnly(8, 30));

            // Act
            var result = calc.CalculateDailyFee(
                dayToll,
                [],
                new HashSet<DateOnly>(),
                new HashSet<VehicleType> { VehicleType.Emergency });

            // Assert
            Assert.True(result.IsSuccess, result.Error);
            Assert.Equal(0, result.Value!.TotalFee);
        }

        [Fact]
        public void WeekendOrHoliday_ShouldReturnZeroFee()
        {
            // Arrange
            var calc = new CongestionTaxCalculator();
            var vehicle = new Vehicle("ABC123", VehicleType.Car);
            var saturday = new DateOnly(2025, 11, 29); // Saturday
            var dayToll = new DayToll(vehicle, "Gothenburg", saturday);
            dayToll.AddPass(new TimeOnly(9, 0));

            // Act
            var result = calc.CalculateDailyFee(
                dayToll,
                [],
                new HashSet<DateOnly> { saturday },
                new HashSet<VehicleType>());

            // Assert
            Assert.True(result.IsSuccess, result.Error);
            Assert.Equal(0, result.Value!.TotalFee);
        }

        [Fact]
        public void NoPasses_ShouldReturnZeroFee()
        {
            // Arrange
            var calc = new CongestionTaxCalculator();
            var vehicle = new Vehicle("ABC123", VehicleType.Car);
            var dayToll = new DayToll(vehicle, "Gothenburg", new DateOnly(2025, 11, 25));

            // Act
            var result = calc.CalculateDailyFee(
                dayToll,
                [],
                new HashSet<DateOnly>(),
                new HashSet<VehicleType>());

            // Assert
            Assert.True(result.IsSuccess, result.Error);
            Assert.Equal(0, result.Value!.TotalFee);
        }

        [Fact]
        public void SingleChargeRule_ShouldChargeHighestFeeWithin60MinuteWindow_AcrossSlots()
        {
            // Arrange
            var calc = new CongestionTaxCalculator();
            var vehicle = new Vehicle("ABC123", VehicleType.Car);
            var dayToll = new DayToll(vehicle, "Gothenburg", new DateOnly(2025, 11, 25));
            dayToll.AddPass(new TimeOnly(7, 45)); // 07:00–07:59 => 18
            dayToll.AddPass(new TimeOnly(8, 10)); // 08:00–08:29 => 13

            var timeSlots = new List<TimeSlot>
            {
                new (new TimeOnly(6, 0), new TimeOnly(6, 29), 8),
                new (new TimeOnly(6, 30), new TimeOnly(6, 59), 13),
                new (new TimeOnly(7, 0), new TimeOnly(7, 59), 18),
                new (new TimeOnly(8, 0), new TimeOnly(8, 29), 13),
                new (new TimeOnly(8, 30), new TimeOnly(14, 59), 8),
                new (new TimeOnly(15, 0), new TimeOnly(15, 29), 13),
                new (new TimeOnly(15, 30), new TimeOnly(16, 59), 18),
                new (new TimeOnly(17, 0), new TimeOnly(17, 59), 13),
                new (new TimeOnly(18, 0), new TimeOnly(18, 29), 8),
                new (new TimeOnly(18, 30), new TimeOnly(23, 59), 0),
                new (new TimeOnly(0, 0), new TimeOnly(5, 59), 0),
            };

            // Act
            var result = calc.CalculateDailyFee(
                dayToll,
                timeSlots,
                new HashSet<DateOnly>(),
                new HashSet<VehicleType>());

            // Assert
            Assert.True(result.IsSuccess, result.Error);
            Assert.Equal(18, result.Value!.TotalFee); // highest fee wins
        }

        [Fact]
        public void DailyFee_ShouldBeCappedAtMaximum()
        {
            // Arrange
            var calc = new CongestionTaxCalculator();
            var vehicle = new Vehicle("ABC123", VehicleType.Car);
            var dayToll = new DayToll(vehicle, "Gothenburg", new DateOnly(2025, 11, 25));

            // Add passes far enough apart to avoid 60-min grouping
            dayToll.AddPass(new TimeOnly(6, 0));   // 8
            dayToll.AddPass(new TimeOnly(7, 10));  // 18
            dayToll.AddPass(new TimeOnly(8, 40));  // 8
            dayToll.AddPass(new TimeOnly(15, 10)); // 13
            dayToll.AddPass(new TimeOnly(16, 40)); // 18

            var timeSlots = new List<TimeSlot>
            {
                new (new TimeOnly(6, 0), new TimeOnly(6, 29), 8),
                new (new TimeOnly(6, 30), new TimeOnly(6, 59), 13),
                new (new TimeOnly(7, 0), new TimeOnly(7, 59), 18),
                new (new TimeOnly(8, 0), new TimeOnly(8, 29), 13),
                new (new TimeOnly(8, 30), new TimeOnly(14, 59), 8),
                new (new TimeOnly(15, 0), new TimeOnly(15, 29), 13),
                new (new TimeOnly(15, 30), new TimeOnly(16, 59), 18),
                new (new TimeOnly(17, 0), new TimeOnly(17, 59), 13),
                new (new TimeOnly(18, 0), new TimeOnly(18, 29), 8),
                new (new TimeOnly(18, 30), new TimeOnly(23, 59), 0),
                new (new TimeOnly(0, 0), new TimeOnly(5, 59), 0),
            };

            // Act
            var result = calc.CalculateDailyFee(
                dayToll,
                timeSlots,
                new HashSet<DateOnly>(),
                new HashSet<VehicleType>(),
                dailyMaxFee: 60);

            // Assert
            Assert.True(result.IsSuccess, result.Error);
            Assert.Equal(60, result.Value!.TotalFee); // capped at 60
        }
    }
}
