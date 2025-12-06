namespace AHA.CongestionTax.Infrastructure.Query.Source1.Tests
{

    using AHA.CongestionTax.Infrastructure.Query.Source1;
    using Microsoft.EntityFrameworkCore;

    public class TestQueryDbContext(DbContextOptions<QueryDbContext> options)
        : QueryDbContext(options)
    {

        /// <summary>
        /// Seeds the given read models into the in-memory database.
        /// </summary>
        /// <typeparam name="TReadModel">The type of the read model entity.</typeparam>
        /// <param name="entities">The entities to seed.</param>
        public void SetData<TReadModel>(IEnumerable<TReadModel> entities) where TReadModel : class
        {
            Set<TReadModel>().AddRange(entities);
            SaveChanges();
        }

        /// <summary>
        /// Creates a new test context with an in-memory database.
        /// </summary>
        public static TestQueryDbContext Create()
        {
            var options = new DbContextOptionsBuilder<QueryDbContext>()
                .UseSqlite("DataSource=:memory:;Cache=Shared")
                .Options;

            return new TestQueryDbContext(options);
        }
    }

}