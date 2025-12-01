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
        public Task<CommandResult<int>> HandleAsync(
            RegisterPassCommand command,
            CancellationToken cancellationToken = default)
        {
            _ = vehicleRepo;
            _ = dayTollRepo;
            _ = ruleSetQueries;
            _ = taxCalculator;

            throw new NotImplementedException();
        }
    }
}