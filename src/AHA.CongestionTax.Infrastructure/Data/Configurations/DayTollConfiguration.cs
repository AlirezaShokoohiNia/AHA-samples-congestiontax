namespace AHA.CongestionTax.Infrastructure.Data.Configurations
{
    using AHA.CongestionTax.Domain.DayTollAgg;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DayTollConfiguration : IEntityTypeConfiguration<DayToll>
    {
        public void Configure(EntityTypeBuilder<DayToll> builder)
        {
            _ = builder.HasKey(c => c.Id);

            _ = builder.HasMany(c => c.Passes)
                        .WithOne()
                        .IsRequired();
        }
    }
}
