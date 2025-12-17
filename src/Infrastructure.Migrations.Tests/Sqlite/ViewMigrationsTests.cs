namespace AHA.CongestionTax.Infrastructure.Migrations.Sqlite.Tests
{
    using AHA.CongestionTax.Infrastructure.Migrations.Sqlite.Tests.Fixtures;
    using Microsoft.EntityFrameworkCore;

    [Collection("Database collection")]
    public class ViewMigrationsTests(DatabaseFixture fixture)
    {

        [Fact]
        public void View_DayTollWithVehicles_Should_Exist()
        {
            // Arrange
            var context = fixture.Context;

            using var connection = context.Database.GetDbConnection();
            connection.Open();

            // Act
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT COUNT(*) FROM vw_DayTollWithVehicles;";
#pragma warning disable CS8605 // Unboxing a possibly null value.
            var result = (long)command.ExecuteScalar(); // SQLite returns Int64
#pragma warning restore CS8605 // Unboxing a possibly null value.

            // Assert
            Assert.True(result >= 0); // proves view exists
        }

    }
}