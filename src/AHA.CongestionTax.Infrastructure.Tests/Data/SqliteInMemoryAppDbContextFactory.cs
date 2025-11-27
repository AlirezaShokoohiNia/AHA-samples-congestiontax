namespace AHA.CongestionTax.Infrastructure.Data.Tests
{
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    public sealed class SqliteInMemoryAppDbContextFactory : IDisposable
    {
        private readonly SqliteConnection _connection;

        private SqliteInMemoryAppDbContextFactory()
        {
            // Shared in-memory SQLite database
            _connection = new SqliteConnection("DataSource=:memory:;Cache=Shared");
            _connection.Open();
        }

        public static AppDbContext CreateContext()
        {
            var factory = new SqliteInMemoryAppDbContextFactory();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(factory._connection)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;

            var context = new AppDbContext(options);

            // Ensure schema is created (once per test session)
            context.Database.EnsureCreated();

            return context;
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();

        }
    }
}