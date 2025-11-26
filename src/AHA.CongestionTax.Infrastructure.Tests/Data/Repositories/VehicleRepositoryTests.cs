namespace AHA.CongestionTax.Infrastructure.Data.Repositories.Tests
{
    using AHA.CongestionTax.Domain.ValueObjects;
    using AHA.CongestionTax.Domain.VehicleAgg;
    using AHA.CongestionTax.Infrastructure.Data.Tests;

    public class VehicleRepositoryTests
    {
        [Fact]
        public async Task Add_Vehicle_And_Find_By_Id_Should_Work()
        {
            // Arrange
            var context = SqliteInMemoryAppDbContextFactory.CreateContext();

            var repo = new VehicleRepository(context);

            var item = new Vehicle(licensePlate: "AAA", vehicleType: VehicleType.Car);

            // Act
            await repo.AddAsync(item);
            await repo.UnitOfWork.CommitAsync();

            var result = await repo.FindByIdAsync(item.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AAA", result!.LicensePlate);
            Assert.Equal(VehicleType.Car, result.VehicleType);

        }
    }
}