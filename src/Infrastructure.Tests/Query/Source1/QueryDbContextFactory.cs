namespace AHA.CongestionTax.Infrastructure.Query.Source1.Tests
{
    using Microsoft.EntityFrameworkCore;

    public static class QueryDbContextTestFactory
    {
        public static QueryDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<QueryDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestQueryDatabase{Guid.NewGuid().ToString()}")
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;

            var context = new QueryDbContext(options);

            // Ensure schema is created
            context.Database.EnsureCreated();

            return context;
        }
    }
}