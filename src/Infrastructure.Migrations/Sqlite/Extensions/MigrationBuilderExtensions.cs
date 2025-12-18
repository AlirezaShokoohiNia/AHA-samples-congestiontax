namespace AHA.CongestionTax.Infrastructure.Migrations.Sqlite.Extensions
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public static class MigrationBuilderExtensions
    {

        // Create the views after tables are created
        public static void CreateViews(this MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE VIEW vw_DayTollWithVehicles AS
    SELECT 
        v.Id As VehicleId,
        v.LicensePlate,
        dt.Date,
        dt.City,
        dt.TotalFee
    FROM DayTolls dt
    JOIN Vehicles v ON dt.VehicleId = v.Id;");

        }

        public static void DropViews(this MigrationBuilder migrationBuilder)
        {
            // Drop the views before dropping tables
            migrationBuilder.Sql(@"DROP VIEW IF EXISTS vw_DayTollWithVehicles;");
        }
    }
}