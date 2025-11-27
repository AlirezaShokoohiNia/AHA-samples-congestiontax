namespace AHA.CongestionTax.Domain.VehicleAgg.Tests
{
    using AHA.CongestionTax.Domain.ValueObjects;

    public class VehicleTests
    {
        [Fact]
        public void Vehicle_ShouldStoreLicensePlateAndType()
        {
            //Arrange

            //Act
            var vehicle = new Vehicle("ABC123", VehicleType.Car);

            //Assert
            Assert.Equal("ABC123", vehicle.LicensePlate);
            Assert.Equal(VehicleType.Car, vehicle.VehicleType);
        }

        [Fact]
        public void Vehicle_ShouldAllowEmergencyType()
        {
            //Arrange

            //Act
            var vehicle = new Vehicle("EMR001", VehicleType.Emergency);

            //Assert
            Assert.Equal(VehicleType.Emergency, vehicle.VehicleType);
        }
    }
}