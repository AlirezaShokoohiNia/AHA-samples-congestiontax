namespace AHA.CongestionTax.Infrastructure.Data.Repositories.Tests
{
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.ValueObjects;
    using AHA.CongestionTax.Domain.VehicleAgg;
    using AHA.CongestionTax.Infrastructure.Data.Tests;

    public class DayTollRepsoitoryTests
    {
        [Fact]
        public async Task Add_DayToll_With_Passes_Should_Persist_Correctly()
        {
            using var db = SqliteInMemoryAppDbContextFactory.CreateContext();

            var vehicleRepo = new VehicleRepository(db);
            var dayTollRepo = new DayTollRepository(db);

            var vehicle = new Vehicle("ABC123", VehicleType.Car);
            await vehicleRepo.AddAsync(vehicle);
            await vehicleRepo.UnitOfWork.CommitAsync();

            var date = new DateOnly(2025, 1, 10);
            var dayToll = new DayToll(vehicle, "Gothenburg", date);

            dayToll.AddPass(new TimeOnly(08, 15));
            dayToll.AddPass(new TimeOnly(09, 30));

            await dayTollRepo.AddAsync(dayToll);
            await dayTollRepo.UnitOfWork.CommitAsync();

            var loaded = await dayTollRepo.FindByIdAsync(dayToll.Id);

            Assert.NotNull(loaded);
            Assert.Equal(2, loaded.Passes.Count);
            Assert.Equal("ABC123", loaded.Vehicle.LicensePlate);
        }

    }
}