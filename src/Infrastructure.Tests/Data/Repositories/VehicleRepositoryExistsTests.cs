namespace AHA.CongestionTax.Infrastructure.Data.Repositories.Tests
{
    using System.Threading.Tasks;
    using Xunit;
    using FluentAssertions;
    using AHA.CongestionTax.Domain.VehicleAgg;
    using AHA.CongestionTax.Domain.ValueObjects;
    using AHA.CongestionTax.Infrastructure.Data.Tests;

    public class VehicleRepositoryExistsTests
    {
        [Fact]
        public async Task ExistsByPlateAsync_ShouldReturnTrue_WhenVehicleExists()
        {
            // Arrange
            using var db = AppDbContextTestFactory.CreateContext();
            var repo = new VehicleRepository(db);

            var vehicle = new Vehicle("AAA111", VehicleType.Car);
            await repo.AddAsync(vehicle);
            await repo.UnitOfWork.CommitAsync();

            // Act
            var result = await repo.ExistsByPlateAsync("AAA111");

            // Assert
            result.IsSuccess.Should().BeTrue(result.Error);
            result.Value.Should().BeTrue();
        }

        [Fact]
        public async Task ExistsByPlateAsync_ShouldReturnFalse_WhenVehicleDoesNotExist()
        {
            // Arrange
            using var db = AppDbContextTestFactory.CreateContext();
            var repo = new VehicleRepository(db);

            // Act
            var result = await repo.ExistsByPlateAsync("ZZZ999");

            // Assert
            result.IsSuccess.Should().BeTrue(result.Error);
            result.Value.Should().BeFalse();
        }

    }
}
