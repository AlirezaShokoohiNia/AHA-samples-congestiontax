namespace AHA.CongestionTax.Infrastructure.Data.Tests
{
    using Microsoft.EntityFrameworkCore;

    public static class AppDbContextTestFactory
    {
        public static AppDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestAppDatabase{Guid.NewGuid().ToString()}")
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .Options;

            var context = new AppDbContext(options);

            // Ensure schema is created
            context.Database.EnsureCreated();

            return context;
        }
    }
}