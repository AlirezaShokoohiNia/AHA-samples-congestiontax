namespace AHA.CongestionTax.Infrastructure.Query.Source1.Tests
{
    using AHA.CongestionTax.Infrastructure.Query.Source1;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    public sealed class SqliteInMemoryQueryDbContextFactory : IDisposable
    {
        private readonly SqliteConnection _connection;

        private SqliteInMemoryQueryDbContextFactory()
        {
            // Shared in-memory SQLite database
            _connection = new SqliteConnection("DataSource=:memory:;Cache=Shared");
            _connection.Open();
        }

        public static TestQueryDbContext CreateContext()
        {
            var factory = new SqliteInMemoryQueryDbContextFactory();
            var options = new DbContextOptionsBuilder<QueryDbContext>()
                .UseSqlite(factory._connection)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;

            var context = new TestQueryDbContext(options);

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