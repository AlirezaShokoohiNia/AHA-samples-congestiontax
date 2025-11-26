namespace AHA.CongestionTax.Infrastructure.Data.Tests
{
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;

    public static class SqliteInMemoryAppDbContextFactory
    {
        private static readonly SqliteConnection _connection;

        static SqliteInMemoryAppDbContextFactory()
        {
            // Shared in-memory SQLite database
            _connection = new SqliteConnection("DataSource=:memory:;Cache=Shared");
            _connection.Open();
        }

        public static AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;

            var context = new AppDbContext(options);

            // Ensure schema is created (once per test session)
            context.Database.EnsureCreated();

            return context;
        }

        public static void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}