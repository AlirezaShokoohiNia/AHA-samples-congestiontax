namespace AHA.CongestionTax.Application.Commands.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Commands;
    using AHA.CongestionTax.Domain.VehicleAgg;
    using Moq;
    using Xunit;
    using FluentAssertions;
    using AHA.CongestionTax.Domain.Abstractions;

    public class RegisterVehicleCommandHandlerTests
    {
        private readonly Mock<IVehicleRepository> _vehicleRepository;
        private readonly RegisterVehicleCommandHandler _handler;

        public RegisterVehicleCommandHandlerTests()
        {
            _vehicleRepository = new Mock<IVehicleRepository>();
            _handler = new RegisterVehicleCommandHandler(_vehicleRepository.Object);
        }

        [Fact]
        public async Task RegisterVehicle_Should_Fail_When_Plate_Exists()
        {
            // Arrange
            var cmd = new RegisterVehicleCommand("ABC123", "Car");

            _vehicleRepository
                .Setup(r => r.ExistsByPlateAsync("ABC123", It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(true));

            // Act
            var result = await _handler.HandleAsync(cmd, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("already exists");
        }

        [Fact]
        public async Task RegisterVehicle_Should_Create_And_Return_Id()
        {
            // Arrange
            var cmd = new RegisterVehicleCommand("XYZ888", "Car");

            _vehicleRepository
                .Setup(r => r.ExistsByPlateAsync("XYZ888", It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(false));

            Vehicle? saved = null;

            _vehicleRepository
                .Setup(r => r.AddAsync(It.IsAny<Vehicle>(), It.IsAny<CancellationToken>()))
                .Callback<Vehicle, CancellationToken>((v, _) => saved = v)
                .ReturnsAsync(Result.Success());

            _vehicleRepository
                .Setup(r => r.UnitOfWork.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(1));

            // Act
            var result = await _handler.HandleAsync(cmd, CancellationToken.None);

            // Assert
            saved.Should().NotBeNull();
            saved!.LicensePlate.Should().Be("XYZ888");

            result.IsSuccess.Should().BeTrue();
            result.Value!.Should().Be(saved.Id);

            _vehicleRepository.Verify(r => r.AddAsync(It.IsAny<Vehicle>(), It.IsAny<CancellationToken>()), Times.Once);
            _vehicleRepository.Verify(r => r.UnitOfWork.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }

}