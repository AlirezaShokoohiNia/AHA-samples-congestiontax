namespace AHA.CongestionTax.Infrastructure.Data.Repositories.Tests
{
    using System.Threading.Tasks;
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
            var addResult = await repo.AddAsync(item);
            Assert.True(addResult.IsSuccess, addResult.Error);

            var commitResult = await repo.UnitOfWork.CommitAsync();
            Assert.True(commitResult.IsSuccess, commitResult.Error);

            var findResult = await repo.FindByIdAsync(item.Id);
            Assert.True(findResult.IsSuccess, findResult.Error);

            var loaded = findResult.Value!;
            // Assert
            Assert.Equal("AAA", loaded.LicensePlate);
            Assert.Equal(VehicleType.Car, loaded.VehicleType);
        }
    }
}
