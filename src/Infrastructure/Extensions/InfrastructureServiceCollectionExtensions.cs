namespace AHA.CongestionTax.Infrastructure.Extensions
{
    using AHA.CongestionTax.Application.Abstractions.Query.Providers;
    using AHA.CongestionTax.Domain.DayTollAgg;
    using AHA.CongestionTax.Domain.Services;
    using AHA.CongestionTax.Domain.VehicleAgg;
    using AHA.CongestionTax.Infrastructure.Data;
    using AHA.CongestionTax.Infrastructure.Data.Repositories;
    using AHA.CongestionTax.Infrastructure.Query.Providers;
    using AHA.CongestionTax.Infrastructure.Query.Source1;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;


    public static class InfrastructureServiceCollectionExtensions
    {
        private class RuleSetOptions
        {
            public string BasePath { get; set; } = string.Empty;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // DbContexts
            _ = services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(config.GetConnectionString("WriteDbConnection")));

            _ = services.AddDbContext<QueryDbContext>(options =>
                options.UseSqlite(config.GetConnectionString("ReadDbConnection")));

            // Options
            _ = services.Configure<RuleSetOptions>(config.GetSection("RuleSet"));

            // Providers
            _ = services.AddScoped<IRuleSetReadProvider>(sp =>
                        {
                            var opts = sp.GetRequiredService<IOptions<RuleSetOptions>>().Value;
                            return new RuleSetReadFileProvider(opts.BasePath);
                        })
                        .AddScoped<IVehicleTaxReadProvider, VehicleTaxReadProvider>()
                        .AddScoped<IVehicleReadProvider, VehicleReadProvider>();

            // Repositories
            _ = services.AddScoped<IVehicleRepository, VehicleRepository>()
                        .AddScoped<IDayTollRepository, DayTollRepository>();

            // Domain Services
            _ = services.AddScoped<ICongestionTaxCalculator, CongestionTaxCalculator>();
            return services;
        }
    }
}