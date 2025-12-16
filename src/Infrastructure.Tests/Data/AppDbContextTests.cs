namespace AHA.CongestionTax.Infrastructure.Data.Tests
{
    using Microsoft.EntityFrameworkCore;

    public class AppDbContextTests
    {
        [Fact]
        public void CanCreateDbContext()
        {
            //Arrange

            //Act
            using var context = AppDbContextTestFactory.CreateContext();

            //Assert
            Assert.NotNull(context);
            Assert.NotNull(context.DayTolls);
            Assert.NotNull(context.Vehicles);
        }
    }
}
