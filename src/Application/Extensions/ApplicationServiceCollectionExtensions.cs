namespace AHA.CongestionTax.Application.Extensions
{
    using AHA.CongestionTax.Application.Abstractions.Command;
    using AHA.CongestionTax.Application.Abstractions.Query;
    using AHA.CongestionTax.Application.Commands;
    using AHA.CongestionTax.Application.DTOs;
    using AHA.CongestionTax.Application.Queries;
    using AHA.CongestionTax.Domain.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            _ = services.AddScoped<ICommandHandler<RegisterPassCommand, int>, RegisterPassCommandHandler>()
                        .AddScoped<ICommandHandler<RegisterVehicleCommand, int>, RegisterVehicleCommandHandler>()
                        .AddScoped<IQueryHandler<GetRuleSetQuery, RuleSetDto>, GetRuleSetQueryHandler>()
                        .AddScoped<IQueryHandler<GetVehicleQuery, VehicleDto>, GetVehicleQueryHandler>()
                        .AddScoped<IQueryHandler<GetVehicleDailyTaxRecordsQuery, IReadOnlyCollection<VehicleDailyTaxDto>>, GetVehicleDailyTaxRecordsQueryHandler>()
                        .AddScoped<IQueryHandler<GetVehicleWeeklyTotalTaxQuery, VehicleTotalTaxDto>, GetVehicleWeeklyTotalTaxQueryHandler>()
                        .AddScoped<ICongestionTaxCalculator, CongestionTaxCalculator>();

            return services;
        }
    }

}