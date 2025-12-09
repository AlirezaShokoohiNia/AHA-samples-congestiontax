namespace AHA.CongestionTax.Infrastructure.Extensions
{
    using AHA.CongestionTax.Application.Abstractions.Query.Providers;
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.Services;
    using AHA.CongestionTax.Domain.VehicleAgg;
    using AHA.CongestionTax.Infrastructure.Data;
    using AHA.CongestionTax.Infrastructure.Data.Repositories;
    using AHA.CongestionTax.Infrastructure.Query.Providers;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // DbContexts
            _ = services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(config.GetConnectionString("DefaultConnection")));

            // Providers
            _ = services.AddScoped<IRuleSetReadProvider, RuleSetReadFileProvider>();

            // Repositories
            _ = services.AddScoped<IVehicleRepository, VehicleRepository>()
                        .AddScoped<IDayTollRepository, DayTollRepository>();

            // Domain Services
            _ = services.AddScoped<ICongestionTaxCalculator, CongestionTaxCalculator>();
            return services;
        }
    }
}