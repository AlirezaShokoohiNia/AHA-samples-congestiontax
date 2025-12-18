namespace AHA.CongestionTax.Infrastructure.Migrations.Extensions
{
    using System.IO;
    using Microsoft.EntityFrameworkCore.Migrations;

    [Obsolete("This class is obsolete and will be removed in future versions. Try using MigrationBuilderExtensions instead.", false)]
    public static class MigrationExtensions
    {
        /// <summary>
        /// Creates a SQL view by executing the contents of a .sql file.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder instance.</param>
        /// <param name="name">The name of the view (and the .sql file without extension).</param>
        /// <exception cref="FileNotFoundException">Thrown if the .sql file does not exist.</exception>
        public static void CreateView(this MigrationBuilder migrationBuilder, string name)
        {
            var path = Path.Combine("Assets", "Schema", "Views", $"{name}.sql");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"SQL file for view '{name}' not found at {path}");
            }

            var sql = File.ReadAllText(path);
            migrationBuilder.Sql(sql);
        }

        /// <summary>
        /// Drops a SQL view with the given name.
        /// </summary>
        /// <param name="migrationBuilder">The migration builder instance.</param>
        /// <param name="name">The name of the view to drop.</param>
        public static void DropView(this MigrationBuilder migrationBuilder, string name)
        {
            migrationBuilder.Sql($"DROP VIEW IF EXISTS {name};");
        }
    }
}
