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
    using AHA.CongestionTax.Infrastructure.Query.Source2.Options;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    public static partial class InfrastructureServiceCollectionExtensions
    {
        [Obsolete("Use AddInfrastructureData, AddInfrastructureSource1, and AddInfrastructureSource2 instead.")]
        public static IServiceCollection AddInfrastructure(
                    this IServiceCollection services,
                    IConfiguration config,
                    bool skipDbContexts = false)
        {
            if (!skipDbContexts)
            {
                // DbContexts
                _ = services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite(config.GetConnectionString("WriteDbConnection")));

                _ = services.AddDbContext<QueryDbContext>(options =>
                    options.UseSqlite(config.GetConnectionString("ReadDbConnection")));
            }

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

        /// <summary>
        /// Registers Infrastructure services for the write side of CQRS, including the
        /// <see cref="AppDbContext"/>.
        /// </summary>
        /// <remarks>
        /// The DbContext provider is supplied lazily via <paramref name="configureData"/>,
        /// allowing the composition root to choose between SQLite, SQL Server, InMemory, etc.
        /// </remarks>
        /// <param name="services">The service collection to extend.</param>
        /// <param name="configureData">
        /// A delegate that configures the <see cref="DbContextOptionsBuilder"/> for <see cref="AppDbContext"/>.
        /// </param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddInfrastructureData(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> configureData)
        {
            //DbContext
            _ = services.AddDbContextFactory<AppDbContext>(configureData)
                        .AddScoped(sp => sp.GetRequiredService<IDbContextFactory<AppDbContext>>().CreateDbContext());

            // Repositories
            _ = services.AddScoped<IVehicleRepository, VehicleRepository>()
                        .AddScoped<IDayTollRepository, DayTollRepository>();

            return services;

        }

        /// <summary>
        /// Registers Infrastructure services for the read side of CQRS backed by <see cref="QueryDbContext"/>.
        /// </summary>
        /// <remarks>
        /// The DbContext provider is supplied lazily via <paramref name="configureSource1"/>,
        /// enabling flexible provider selection (SQLite, SQL Server, InMemory) at the composition root.
        /// This method also wires up read-side providers.
        /// </remarks>
        /// <param name="services">The service collection to extend.</param>
        /// <param name="configureSource1">
        /// A delegate that configures the <see cref="DbContextOptionsBuilder"/> for <see cref="QueryDbContext"/>.
        /// </param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddInfrastructureSource1(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> configureSource1)
        {
            //DbContext
            _ = services.AddDbContextFactory<QueryDbContext>(configureSource1)
                        .AddScoped(sp => sp.GetRequiredService<IDbContextFactory<QueryDbContext>>().CreateDbContext());

            // Providers
            _ = services.AddScoped<IVehicleTaxReadProvider, VehicleTaxReadProvider>()
                        .AddScoped<IVehicleReadProvider, VehicleReadProvider>();

            return services;

        }

        /// <summary>
        /// Registers Infrastructure services for read-side rule set provider that source data from JSON files.
        /// </summary>
        /// <remarks>
        /// This method wires up providers such as <see cref="IRuleSetReadProvider"/> using
        /// <see cref="RuleSetOptions"/> supplied from the composition root.
        /// </remarks>
        /// <param name="services">The service collection to extend.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddInfrastructureSource2(
            this IServiceCollection services
        )
        {
            // Provider
            _ = services.AddScoped<IRuleSetReadProvider>(sp =>
                        {
                            var opts = sp.GetRequiredService<IOptions<RuleSetOptions>>().Value;
                            return new RuleSetReadFileProvider(opts.BasePath);
                        });

            return services;
        }
    }
}