namespace AHA.CongestionTax.Infrastructure.Migrations.Sqlite
{
    using AHA.CongestionTax.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class MigrationsExtensions
    {
        /// <summary>
        /// Applies all pending migrations for <see cref="AppDbContext"/> using the current service provider.
        /// Logs errors instead of crashing the host.
        /// </summary>
        public static void ApplySqliteMigrations(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            ctx.Database.Migrate();
        }
    }
}
