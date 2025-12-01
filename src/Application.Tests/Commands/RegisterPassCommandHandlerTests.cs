namespace AHA.CongestionTax.Application.Commands.Tests
{
    using AHA.CongestionTax.Application.Commands;
    using AHA.CongestionTax.Application.ReadModels.Queries;
    using AHA.CongestionTax.Application.ReadModels.Queries.RuleSets;
    using AHA.CongestionTax.Domain.Abstractions;
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.Services;
    using AHA.CongestionTax.Domain.ValueObjects;
    using AHA.CongestionTax.Domain.VehicleAgg;
    using FluentAssertions;
    using Moq;

    public class RegisterPassCommandHandlerTests
    {
        private readonly Mock<IVehicleRepository> _vehicleRepo;
        private readonly Mock<IDayTollRepository> _dayTollRepo;
        private readonly Mock<IRuleSetQueries> _ruleSetQueries;
        private readonly Mock<ICongestionTaxCalculator> _taxCalculator;
        private readonly RegisterPassCommandHandler _handler;

        public RegisterPassCommandHandlerTests()
        {
            _vehicleRepo = new Mock<IVehicleRepository>();
            _dayTollRepo = new Mock<IDayTollRepository>();
            _ruleSetQueries = new Mock<IRuleSetQueries>();
            _taxCalculator = new Mock<ICongestionTaxCalculator>();

            _handler = new(_vehicleRepo.Object, _dayTollRepo.Object, _ruleSetQueries.Object, _taxCalculator.Object);
        }

        [Fact]
        public async Task RegisterPass_Should_Fail_When_Vehicle_NotFound()
        {
            // Arrange
            var cmd = new RegisterPassCommand("NOPE123", DateTime.UtcNow, "Gothenburg");

            _vehicleRepo
                .Setup(r => r.GetByPlateAsync("NOPE123", It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<Vehicle>("Vehicle not found"));

            // Act
            var result = await _handler.HandleAsync(cmd, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().Contain("not found");
        }

        [Fact]
        public async void RegisterPass_Should_Create_New_DayToll_When_NotExists()
        {
            // Arrange
            var cmd = new RegisterPassCommand("ABC123", new DateTime(2025, 11, 30, 8, 0, 0), "Gothenburg");
            var vehicle = new Vehicle("ABC123", VehicleType.Car);

            _vehicleRepo
                .Setup(r => r.GetByPlateAsync("ABC123", It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(vehicle));

            _dayTollRepo
                .Setup(r => r.GetByVehicleAndCityAndDateAsync(vehicle.Id, cmd.City, DateOnly.FromDateTime(cmd.Timestamp), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Failure<DayToll>("Not found"));

            _dayTollRepo
                .Setup(r => r.AddAsync(It.IsAny<DayToll>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success());

            _ruleSetQueries
                .Setup(q => q.GetRulesForCityAsync(cmd.City))
                .ReturnsAsync(ReadModelsQueryResult.Success(new RuleSetReadModel
                {
                    City = cmd.City,
                    TimeSlots = [],
                    Holidays = [],
                    TollFreeVehicles = []
                }));

            _taxCalculator
                .Setup(c => c.CalculateDailyFee(It.IsAny<DayToll>(), It.IsAny<IReadOnlyCollection<TimeSlot>>(),
                                                It.IsAny<IReadOnlySet<DateOnly>>(), It.IsAny<IReadOnlySet<VehicleType>>(), It.IsAny<int>()))
                .Returns(Result.Success(new DailyTaxResult(30, [])));

            _dayTollRepo
                .Setup(r => r.UnitOfWork.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(1));

            // Act
            var result = await _handler.HandleAsync(cmd, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(30); // daily fee
            _dayTollRepo.Verify(r => r.AddAsync(It.IsAny<DayToll>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async void RegisterPass_Should_Update_Existing_DayToll_And_Recalculate_Fee()
        {
            // Arrange
            var cmd = new RegisterPassCommand("XYZ999", new DateTime(2025, 11, 30, 9, 0, 0), "Gothenburg");
            var vehicle = new Vehicle("XYZ999", VehicleType.Car);
            var dayToll = new DayToll(vehicle, cmd.City, DateOnly.FromDateTime(cmd.Timestamp));

            _vehicleRepo
                .Setup(r => r.GetByPlateAsync("XYZ999", It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(vehicle));

            _dayTollRepo
                .Setup(r => r.GetByVehicleAndCityAndDateAsync(vehicle.Id, cmd.City, DateOnly.FromDateTime(cmd.Timestamp), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(dayToll));

            _ruleSetQueries
                .Setup(q => q.GetRulesForCityAsync(cmd.City))
                .ReturnsAsync(ReadModelsQueryResult.Success(new RuleSetReadModel
                {
                    City = cmd.City,
                    TimeSlots = [],
                    Holidays = [],
                    TollFreeVehicles = []
                }));

            _taxCalculator
                .Setup(c => c.CalculateDailyFee(dayToll, It.IsAny<IReadOnlyCollection<TimeSlot>>(),
                                                It.IsAny<IReadOnlySet<DateOnly>>(), It.IsAny<IReadOnlySet<VehicleType>>(), It.IsAny<int>()))
                .Returns(Result.Success(new DailyTaxResult(45, [])));

            _dayTollRepo
                .Setup(r => r.UnitOfWork.CommitAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Success(1));

            // Act
            var result = await _handler.HandleAsync(cmd, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(45);
            dayToll.Passes.Should().ContainSingle(p => p.Time == TimeOnly.FromDateTime(cmd.Timestamp));

        }
    }
}