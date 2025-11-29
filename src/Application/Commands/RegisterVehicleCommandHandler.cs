namespace AHA.CongestionTax.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions;
    using AHA.CongestionTax.Domain.VehicleAgg;

    public class RegisterVehicleCommandHandler(
        IVehicleRepository vehicleRepository)
        : ICommandHandler<RegisterVehicleCommand, int>
    {
        public Task<CommandResult<int>> HandleAsync(RegisterVehicleCommand command, CancellationToken cancellationToken) => throw new NotImplementedException();
    }
}