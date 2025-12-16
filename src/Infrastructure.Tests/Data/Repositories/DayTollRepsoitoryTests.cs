namespace AHA.CongestionTax.Infrastructure.Data.Repositories.Tests
{
    using System.Threading.Tasks;
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.ValueObjects;
    using AHA.CongestionTax.Domain.VehicleAgg;
    using AHA.CongestionTax.Infrastructure.Data.Tests;

    public class DayTollRepositoryTests
    {
        [Fact]
        public async Task Add_DayToll_With_Passes_Should_Persist_Correctly()
        {
            using var db = AppDbContextTestFactory.CreateContext();

            var vehicleRepo = new VehicleRepository(db);
            var dayTollRepo = new DayTollRepository(db);

            var vehicle = new Vehicle("ABC123", VehicleType.Car);
            var addVehicleResult = await vehicleRepo.AddAsync(vehicle);
            Assert.True(addVehicleResult.IsSuccess, addVehicleResult.Error);

            var commitVehicleResult = await vehicleRepo.UnitOfWork.CommitAsync();
            Assert.True(commitVehicleResult.IsSuccess, commitVehicleResult.Error);

            var date = new DateOnly(2025, 1, 10);
            var dayToll = new DayToll(vehicle, "Gothenburg", date);

            dayToll.AddPass(new TimeOnly(08, 15));
            dayToll.AddPass(new TimeOnly(09, 30));

            var addDayTollResult = await dayTollRepo.AddAsync(dayToll);
            Assert.True(addDayTollResult.IsSuccess, addDayTollResult.Error);

            var commitDayTollResult = await dayTollRepo.UnitOfWork.CommitAsync();
            Assert.True(commitDayTollResult.IsSuccess, commitDayTollResult.Error);

            var loadedResult = await dayTollRepo.FindByIdAsync(dayToll.Id);
            Assert.True(loadedResult.IsSuccess, loadedResult.Error);

            var loaded = loadedResult.Value!;
            Assert.Equal(2, loaded.Passes.Count);
            Assert.Equal("ABC123", loaded.Vehicle.LicensePlate);
        }

        [Fact]
        public async Task Add_DayToll_And_Get_By_VehicleId_And_City_And_PassDate_Should_Found()
        {
            // Arrange
            var city = "Gothenburg";
            var context = AppDbContextTestFactory.CreateContext();
            var dayTollRepo = new DayTollRepository(context);
            var vehicleRepo = new VehicleRepository(context);

            var vehicle = new Vehicle("ABC123", VehicleType.Car);
            var addVehicleResult = await vehicleRepo.AddAsync(vehicle);
            Assert.True(addVehicleResult.IsSuccess, addVehicleResult.Error);

            var commitVehicleResult = await vehicleRepo.UnitOfWork.CommitAsync();
            Assert.True(commitVehicleResult.IsSuccess, commitVehicleResult.Error);

            var passDate = new DateOnly(2025, 1, 10);
            var dayToll = new DayToll(vehicle, city, passDate);

            var addDayTollResult = await dayTollRepo.AddAsync(dayToll);
            Assert.True(addDayTollResult.IsSuccess, addDayTollResult.Error);

            var commitDayTollResult = await dayTollRepo.UnitOfWork.CommitAsync();
            Assert.True(commitDayTollResult.IsSuccess, commitDayTollResult.Error);

            // Act
            var loadedResult = await dayTollRepo.GetByVehicleAndCityAndDateAsync(vehicle.Id, city, passDate);

            //Assert
            Assert.True(loadedResult.IsSuccess, loadedResult.Error);

            var loaded = loadedResult.Value!;
            Assert.Equal(vehicle.Id, loaded.Vehicle.Id);
            Assert.Equal(city, loaded.City);
            Assert.Equal(passDate, loaded.Date);

        }
    }
}
