using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Payment.API.EntityConfiguration;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment.API.Entities.Payment>
{
    public void Configure(EntityTypeBuilder<Entities.Payment> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).UseSerialColumn();
        builder.ToTable("Payments", schema: "sales");
        builder.Property(p => p.Amount).HasColumnType("decimal(18,2)").IsRequired();
    }
}