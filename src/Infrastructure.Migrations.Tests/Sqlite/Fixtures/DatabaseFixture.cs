namespace AHA.CongestionTax.Infrastructure.Migrations.Sqlite.Tests.Fixtures
{
    using AHA.CongestionTax.Infrastructure.Data;
    using AHA.CongestionTax.Infrastructure.Migrations.Sqlite;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Fixture for setting up and tearing down the test Sqlite database.
    /// </summary>
    public sealed class DatabaseFixture : IDisposable
    {
        public AppDbContext Context { get; }

        /// <summary>
        /// Creates and migrates the test Sqlite database.     
        /// </summary>
        public DatabaseFixture()
        {
            // Ensure clean DB once
            var factory = new AppDbContextMigrationFactory();
            Context = factory.CreateDbContext([]);
            Context.Database.Migrate();

        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }

}