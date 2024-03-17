using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.API.EntityConfiguration;

public class PaymentHistoryConfiguration : IEntityTypeConfiguration<Entities.PaymentHistory>
{
    public void Configure(EntityTypeBuilder<Entities.PaymentHistory> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).UseSerialColumn();
        builder.ToTable("PaymentHistories", schema: "sales");
        builder.Property(p => p.Amount).HasColumnType("decimal(18,2)").IsRequired();
    }
}