namespace AHA.CongestionTax.Api.Extensions
{
    using Microsoft.Data.Sqlite;
    using Microsoft.Extensions.Configuration;

    /// <remarks>
    /// Handles converting relative paths to absolute paths for SQLite database files.
    /// </remarks>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Gets the absolute connection string for the write database.
        /// </summary>
        public static string GetWriteDbConnectionString(this IConfiguration configuration)
        {
            var configured = configuration.GetConnectionString("WriteDbConnection");
            // "Data Source=Sqlite/congestiontax.db"
            // "congestiontax.db"

            var relative = configured!.Replace("Data Source=", "").Trim();
            var absolute = Path.Combine(AppContext.BaseDirectory, relative);
            Directory.CreateDirectory(Path.GetDirectoryName(absolute)!);

            var writeDbConnStr = new SqliteConnectionStringBuilder
            {
                DataSource = absolute
            }.ToString();
            return writeDbConnStr;
        }

        /// <summary>
        /// Gets the absolute connection string for the read database.
        /// </summary>
        public static string GetReadDbConnectionString(this IConfiguration configuration)
        {
            var configured = configuration.GetConnectionString("ReadDbConnection");
            // "Data Source=Sqlite/congestiontax.db"
            // "congestiontax.db"

            var relative = configured!.Replace("Data Source=", "").Trim();
            var absolute = Path.Combine(AppContext.BaseDirectory, relative);
            Directory.CreateDirectory(Path.GetDirectoryName(absolute)!);

            var readDbConnStr = new SqliteConnectionStringBuilder
            {
                DataSource = absolute
            }.ToString();
            return readDbConnStr;
        }
    }
}