namespace AHA.CongestionTax.Infrastructure.Data.Configurations
{
    using AHA.CongestionTax.Domain.VehicleAgg;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            _ = builder.HasKey(c => c.Id);

        }
    }
}
