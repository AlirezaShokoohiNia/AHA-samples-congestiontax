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
        public void Configure(EntityTypeBuilder<VehicleReadModel> builder)
        {
            _ = builder.ToTable("Vehicle")
                       .HasKey(v => v.VehicleId);
        }

    }
}