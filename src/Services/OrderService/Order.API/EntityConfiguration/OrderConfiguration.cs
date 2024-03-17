using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Order.API.EntityConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<Entities.Order>
{
    public void Configure(EntityTypeBuilder<Entities.Order> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).UseSerialColumn();
        builder.ToTable("Orders", schema: "sales");
        builder.Property(p => p.TotalAmount).HasColumnType("decimal(18,2)").IsRequired();
    }
}