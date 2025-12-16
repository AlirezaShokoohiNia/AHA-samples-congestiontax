namespace AHA.CongestionTax.Api.Endpoints.Tests
{
    using AHA.CongestionTax.Infrastructure.Extensions;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// Provides a custom <see cref="WebApplicationFactory{TEntryPoint}"/> for integration testing.
    /// </summary>
    /// <remarks>
    /// This factory configures the application host to run in a dedicated <c>Test</c> environment.
    /// It overrides configuration and service registrations to ensure test isolation:
    /// <list type="bullet">
    ///   <item>
    ///     <description>Replaces <c>appsettings.json</c> with in‑memory configuration values (e.g. <c>RuleSet:BasePath</c>).</description>
    ///   </item>
    ///   <item>
    ///     <description>Registers <c>AppDbContext</c> and <c>QueryDbContext</c> with EF Core InMemory providers, each using a unique database name per test run.</description>
    ///   </item>
    ///   <item>
    ///     <description>Ensures that write‑side and read‑side contexts are isolated, avoiding cross‑test contamination.</description>
    ///   </item>
    /// </list>
    /// This setup allows endpoint tests to exercise orchestration and wiring without relying on external infrastructure.
    /// </remarks>
    public class EndpointTestWebApplicationFactory : WebApplicationFactory<Program>
    {

        /// <summary>
        /// Configures the web host for integration tests.
        /// </summary>
        /// <param name="builder">The web host builder used to configure services and environment.</param>
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Run in a dedicated "Test" environment
            builder.UseEnvironment("Test");

            // Override appsettings.json with in-memory configuration for predictable test behavior
            builder.ConfigureAppConfiguration((ctx, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["RuleSet:BasePath"] = "TestData"
                });
            });

            // Replace infrastructure services with EF Core InMemory providers
            builder.ConfigureTestServices(services =>
            {
                // Write side (commands)
                services.AddInfrastructureData(opts =>
                    opts.UseInMemoryDatabase($"AppDb{Guid.NewGuid()}"));

                // Read side (queries)
                services.AddInfrastructureSource1(opts =>
                    opts.UseInMemoryDatabase($"QueryDb{Guid.NewGuid()}"));
            });
        }
    }
}
