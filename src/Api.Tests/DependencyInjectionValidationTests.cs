namespace AHA.CongestionTax.Api.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using AHA.CongestionTax.Application.Extensions;
    using AHA.CongestionTax.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Builder;
    using AHA.CongestionTax.Infrastructure.Data;
    using AHA.CongestionTax.Application.Abstractions.Command;
    using AHA.CongestionTax.Application.Abstractions.Query;
    using FluentAssertions;
    using Microsoft.EntityFrameworkCore;
    using AHA.CongestionTax.Infrastructure.Query.Providers;
    using AHA.CongestionTax.Application.Abstractions.Query.Providers;
    using AHA.CongestionTax.Infrastructure.Query.Source1;

    public class DependencyInjectionValidationTests
    {
        private readonly IServiceProvider _provider;

        public DependencyInjectionValidationTests()
        {
            var builder = WebApplication.CreateBuilder();
            _ = builder
                    .Services
                        .AddApplication()
                        .AddInfrastructure(builder.Configuration, skipDbContexts: false);

            // Add test registrations
            _ = builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            _ = builder.Services.AddDbContext<QueryDbContext>(options =>
                options.UseInMemoryDatabase("TestQueryDb"));

            // Override IRuleSetReadProvider registration for tests
            _ = builder.Services.AddScoped<IRuleSetReadProvider>(sp =>
            new RuleSetReadFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "TestData")));

            _provider = builder.Services.BuildServiceProvider(validateScopes: true);
        }

        [Fact]
        public void CanResolve_DbContext()
        {
            var appDbContextType = typeof(AppDbContext);
            AssertServiceResolvable(appDbContextType);

            var queryDbContextType = typeof(QueryDbContext);
            AssertServiceResolvable(queryDbContextType);
        }
        [Fact]

        public void CanResolve_AllHandlers()
        {

            var handlerInterfaces = typeof(ApplicationServiceCollectionExtensions).Assembly
                .GetTypes()
                .SelectMany(t => t.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                                (i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                                 i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))))
                .ToList();

            foreach (var iface in handlerInterfaces)
            {
                AssertServiceResolvable(iface);
            }

        }

        private void AssertServiceResolvable(Type serviceType)
        {
            using var scope = _provider.CreateScope();
            var sp = scope.ServiceProvider;

            try
            {
                var service = sp.GetRequiredService(serviceType);
                service.Should().NotBeNull();
            }
            catch (Exception ex)
            {
                throw new Xunit.Sdk.XunitException(
                    $"Failed to resolve {serviceType.FullName}\n" +
                    $"Exception: {ex.GetType().Name}\n" +
                    $"Message: {ex.Message}");
            }
        }


    }
}
