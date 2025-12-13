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
    using AHA.CongestionTax.Infrastructure.Query.Source1;
    using AHA.CongestionTax.Infrastructure.Query.Source2.Options;

    public class DependencyInjectionValidationTests
    {
        private readonly IServiceProvider _provider;

        public DependencyInjectionValidationTests()
        {
            var builder = WebApplication.CreateBuilder();

            // Application services
            builder.Services.AddApplication();

            // Infrastructure: write side (AppDbContext) with InMemory
            builder.Services.AddInfrastructureData(opts =>
                opts.UseInMemoryDatabase("TestWriteDb"));

            // Infrastructure: read side (QueryDbContext) with InMemory
            builder.Services.AddInfrastructureSource1(opts =>
                opts.UseInMemoryDatabase("TestReadDb"));

            // Infrastructure: JSON/file providers
            builder.Services.AddInfrastructureSource2();

            // Override RuleSetOptions for tests
            builder.Services.Configure<RuleSetOptions>(opts =>
            {
                opts.BasePath = Path.Combine(Directory.GetCurrentDirectory(), "TestData");
            });

            _provider = builder.Services.BuildServiceProvider(validateScopes: true);
        }

        [Fact]
        public void CanResolve_DbContext()
        {
            AssertServiceResolvable(typeof(AppDbContext));
            AssertServiceResolvable(typeof(QueryDbContext));
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
