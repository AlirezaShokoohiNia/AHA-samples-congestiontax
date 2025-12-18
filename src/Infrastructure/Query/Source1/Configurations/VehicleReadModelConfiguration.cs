namespace AHA.CongestionTax.Infrastructure.Query.Source1.Configurations
{
    using AHA.CongestionTax.Infrastructure.Query.Source1.ReadModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// EF Core configuration for the Vehicle read model.
    /// </summary>
    public sealed class VehicleReadModelConfiguration
        : IEntityTypeConfiguration<VehicleReadModel>
    {
        /// <summary>
        /// Configures the Vehicle read model
        /// </summary>
        /// <remarks>
        /// <code>
        /// CREATE VIEW vw_Vehicles AS
        ///     SELECT 
        ///         v.Id As VehicleId
        ///         v.LicensePlate,
        ///         dt.VehicleType
        ///     FROM Vehicle v;
        /// </code>
        /// </remarks>

        public void Configure(EntityTypeBuilder<VehicleReadModel> builder)
        {
            _ = builder.ToView("vw_Vehicles")
                       .HasKey(v => v.VehicleId);
        }

    }
}