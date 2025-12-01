namespace AHA.CongestionTax.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions;
    using AHA.CongestionTax.Application.ReadModels.Queries;
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.Services;
    using AHA.CongestionTax.Domain.VehicleAgg;

    public class RegisterPassCommandHandler(
        IVehicleRepository vehicleRepo,
        IDayTollRepository dayTollRepo,
        IRuleSetQueries ruleSetQueries,
        ICongestionTaxCalculator taxCalculator)
        : ICommandHandler<RegisterPassCommand, int>
    {
        public async Task<CommandResult<int>> HandleAsync(
            RegisterPassCommand command,
            CancellationToken cancellationToken = default)
        {
            // Step 1: Check Vehicle aggregate
            var vehicleResult = await vehicleRepo.GetByPlateAsync(command.LicensePlate, cancellationToken);
            if (!vehicleResult.IsSuccess)
                return CommandResult.Failure<int>(vehicleResult.Error!);

            if (vehicleResult.Value is null)
                return CommandResult.Failure<int>($"Vehicle with plate {command.LicensePlate} not found.");

            _ = dayTollRepo;
            _ = ruleSetQueries;
            _ = taxCalculator;

            // For now, stop here. Later steps will handle DayToll, rulesets, fee calculation, etc.
            return CommandResult.Failure<int>("Handler not fully implemented yet.");

        }
    }
}