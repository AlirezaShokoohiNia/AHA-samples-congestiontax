namespace AHA.CongestionTax.Infrastructure.Migrations.Sqlite
{
    using AHA.CongestionTax.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Factory for creating AppDbContext instances during design time for migrations.
    /// </summary>
    public class AppDbContextMigrationFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        /// <summary>
        /// Ensures that the database file is deleted if it exists to start fresh. 
        /// </summary>
        private static void EnsureCleanDatabase(string dbPath)
        {
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }
        }

        /// <summary>
        /// Creates a new instance of AppDbContext with SQLite configuration for design-time use.
        /// </summary>
        public AppDbContext CreateDbContext(string[] args)
        {
            var dbName = "testcongestiontax.db";
            var dbPath = Path.Combine(AppContext.BaseDirectory, dbName);

            // Ensure fresh DB
            EnsureCleanDatabase(dbPath);

            var migrationsAssembly = typeof(AppDbContextMigrationFactory).Assembly.GetName().Name;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            _ = builder.UseSqlite($"Data Source={dbPath}", o => o.MigrationsAssembly(migrationsAssembly))
                        .EnableDetailedErrors()
                        .EnableSensitiveDataLogging()
                        .LogTo(Console.WriteLine, LogLevel.Information);
            ;

            return new AppDbContext(builder.Options);
        }
    }
}