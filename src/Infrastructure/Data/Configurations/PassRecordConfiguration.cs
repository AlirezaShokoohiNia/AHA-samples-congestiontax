namespace AHA.CongestionTax.Infrastructure.Data.Configurations
{
    using AHA.CongestionTax.Domain.DayTollAgg;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PassRecordConfiguration : IEntityTypeConfiguration<PassRecord>
    {
        public void Configure(EntityTypeBuilder<PassRecord> builder)
        {
            _ = builder.HasKey(x => x.Id);

            _ = builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            // Backlink defined in CartItem
            _ = builder.HasOne<DayToll>()
                   .WithMany(c => c.Passes)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
