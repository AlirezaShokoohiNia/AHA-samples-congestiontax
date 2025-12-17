namespace AHA.CongestionTax.Infrastructure.Query.Source1.Configurations
{
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// EF Core configuration for the Vehicle read model.
    /// </summary>
    public sealed class DayTollReadModelConfiguration
        : IEntityTypeConfiguration<DayTollReadModel>
    {
        /// <summary>
        /// Configures the Vehicle read model
        /// </summary>
        /// <remarks>
        /// <code>
        /// CREATE VIEW vw_DayTollWithVehicle AS
        ///     SELECT 
        ///         v.Id As VehicleId
        ///         v.LicensePlate,
        ///         dt.Date,
        ///         dt.City,
        ///         dt.TotalFee,
        ///     FROM DayToll dt
        ///     JOIN Vehicle v ON dt.VehicleId = v.Id;
        /// </code>
        /// </remarks>
        public void Configure(EntityTypeBuilder<DayTollReadModel> builder)
        {
            _ = builder.ToView("vw_DayTollWithVehicles")
                       .HasKey(dt => new { dt.VehicleId, dt.Date, dt.City });
        }

    }
}