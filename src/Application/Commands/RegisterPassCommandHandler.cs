namespace AHA.CongestionTax.Application.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using AHA.CongestionTax.Application.Abstractions.Adapter;
    using AHA.CongestionTax.Application.Abstractions.Command;
    using AHA.CongestionTax.Application.Abstractions.Query;
    using AHA.CongestionTax.Application.Adapters;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Application.Mappers;
    using AHA.CongestionTax.Application.Queries;
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.Services;
    using AHA.CongestionTax.Domain.VehicleAgg;

    public class RegisterPassCommandHandler(
        IVehicleRepository vehicleRepo,
        IDayTollRepository dayTollRepo,
        IQueryHandler<GetRuleSetQuery, RuleSetDto> getRuleSetQueryHandler,
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

            var vehicle = vehicleResult.Value;
            var date = DateOnly.FromDateTime(command.Timestamp);
            var time = TimeOnly.FromDateTime(command.Timestamp);

            // Step 2: Get DayToll aggregate
            var dayTollResult = await dayTollRepo.GetByVehicleAndCityAndDateAsync(vehicle.Id, command.City, date, cancellationToken);

            DayToll dayToll;
            if (!dayTollResult.IsSuccess || dayTollResult.Value is null)
            {
                // Create new DayToll if not found
                dayToll = new DayToll(vehicle, command.City, date);
                var addResult = await dayTollRepo.AddAsync(dayToll, cancellationToken);
                if (!addResult.IsSuccess)
                    return CommandResult.Failure<int>(addResult.Error!);
            }
            else
            {
                dayToll = dayTollResult.Value;
            }

            // Step 3: Add pass
            dayToll.AddPass(time);

            // Step 4: Get ruleset
            var getRuleSetQuery = new GetRuleSetQuery(command.City);
            var rulesResult = await getRuleSetQueryHandler.HandleAsync(getRuleSetQuery, cancellationToken);
            if (!rulesResult.IsSuccess || rulesResult.Value is null)
                return CommandResult.Failure<int>(rulesResult.Error ?? $"Ruleset not found for city {command.City}");

            var rules = rulesResult.Value;
            var timeSlots = MappingHelper.MapEach(
                    rules.TimeSlots,
                    TimeSlotRuleDtoToTimeSlotAdapter.Adapt)
                    .ToList();
            var holidayDates = MappingHelper.MapEach(
                    rules.Holidays,
                    HolidayRuleDtoToDatesMapper.Map)
                    .SelectMany(d => d)
                    .ToHashSet();
            var tollFreeVehicleTypes = MappingHelper.MapEach(
                    rules.TollFreeVehicles,
                    VehicleFreeRuleDtoToVehicleTypeAdapter.Adapt)
                    .ToHashSet();

            // Step 5: Calculate fee
            var calcResult = taxCalculator.CalculateDailyFee(
                dayToll,
                timeSlots,
                holidayDates,
                tollFreeVehicleTypes,
                60);

            if (!calcResult.IsSuccess)
                return CommandResult.Failure<int>(calcResult.Error ?? "Fee calculation failed");

            var dailyFee = calcResult.Value.TotalFee;

            // Step 6: Apply fee
            dayToll.ApplyCalculatedFee(dailyFee);

            // Step 7: Commit
            var commitResult = await dayTollRepo.UnitOfWork.CommitAsync(cancellationToken);
            if (!commitResult.IsSuccess)
                return CommandResult.Failure<int>(commitResult.Error!);

            return CommandResult<int>.Success(dailyFee);
        }
    }
}