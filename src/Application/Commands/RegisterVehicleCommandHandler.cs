namespace AHA.CongestionTax.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions.Command;
    using AHA.CongestionTax.Domain.ValueObjects;
    using AHA.CongestionTax.Domain.VehicleAgg;

    public class RegisterVehicleCommandHandler(
        IVehicleRepository vehicleRepository)
        : ICommandHandler<RegisterVehicleCommand, int>
    {
        public async Task<CommandResult<int>> HandleAsync(
            RegisterVehicleCommand command,
            CancellationToken cancellationToken)
        {
            var existsResult = await vehicleRepository.ExistsByPlateAsync(command.LicensePlate,
                                                                          cancellationToken);
            if (!existsResult.IsSuccess)
                return CommandResult.Failure<int>(existsResult.Error!);

            if (existsResult.Value)
                return CommandResult.Failure<int>($"Vehicle with plate {command.LicensePlate} already exists.");

            var vehicle = new Vehicle(command.LicensePlate,
                                      Enum.Parse<VehicleType>(command.VehicleType));
            var addResult = await vehicleRepository.AddAsync(vehicle, cancellationToken);
            if (!addResult.IsSuccess)
                return CommandResult.Failure<int>(addResult.Error!);

            var commitResult = await vehicleRepository.UnitOfWork.CommitAsync(cancellationToken);
            if (!commitResult.IsSuccess)
                return CommandResult.Failure<int>(commitResult.Error!);

            return CommandResult.Success(vehicle.Id);
        }
    }
}