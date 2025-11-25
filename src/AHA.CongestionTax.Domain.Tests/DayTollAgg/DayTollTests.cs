namespace AHA.CongestionTax.Domain.DayTollAgg.Tests
{
    using AHA.CongestionTax.Domain.ValueObjects;
    using AHA.CongestionTax.Domain.VehicleAgg;

    public class DayTollTests
    {
        [Fact]
        public void DayToll_ShouldStoreVehicleCityAndDate()
        {
            //Arrange
            var vehicle = new Vehicle("ABC123", VehicleType.Car);

            //Act
            var dayToll = new DayToll(vehicle, "Gothenburg", new DateOnly(2025, 11, 25));

            //Assert
            Assert.Equal(vehicle, dayToll.Vehicle);
            Assert.Equal("Gothenburg", dayToll.City);
            Assert.Equal(new DateOnly(2025, 11, 25), dayToll.Date);
        }

        [Fact]
        public void AddPass_ShouldIncreasePassCount()
        {
            //Arrange
            var vehicle = new Vehicle("ABC123", VehicleType.Car);
            var dayToll = new DayToll(vehicle, "Gothenburg", DateOnly.FromDateTime(DateTime.Now));

            //Act
            dayToll.AddPass(new TimeOnly(8, 30));

            //Assert
            Assert.Single(dayToll.Passes);
            Assert.Equal(new TimeOnly(8, 30), dayToll.Passes.First().Time);
        }

        [Fact]
        public void ApplyCalculatedFee_ShouldThrowIfNegative()
        {
            //Arrange
            var vehicle = new Vehicle("ABC123", VehicleType.Car);
            var dayToll = new DayToll(vehicle, "Gothenburg", DateOnly.FromDateTime(DateTime.Now));

            //Act

            //Assert
            Assert.Throws<DomainException>(() => dayToll.ApplyCalculatedFee(-10));
        }

        [Fact]
        public void ApplyCalculatedFee_ShouldSetTotalFee()
        {
            //Arrange
            var vehicle = new Vehicle("ABC123", VehicleType.Car);
            var dayToll = new DayToll(vehicle, "Gothenburg", DateOnly.FromDateTime(DateTime.Now));

            //Act
            dayToll.ApplyCalculatedFee(50);

            //Assert
            Assert.Equal(50, dayToll.TotalFee);
        }
    }

}