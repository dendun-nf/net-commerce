using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net_Ecommerce.Features.Orders;

namespace Net_Ecommerce.Data.config;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id); {builder.Property(o => o.Id).ValueGeneratedOnAdd(); }
        builder.Property(o => o.Username).HasColumnType("varchar(25)");
        builder.Property(o => o.Email).HasColumnType("varchar(100)");
        builder.Property(o => o.TotalPrice).HasColumnType("decimal(18,2)");

        builder.HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId);
        builder.HasOne(o => o.Seller).WithMany(s => s.Orders).HasForeignKey(o => o.SellerId);
        builder.OwnsMany(o => o.Items, odb => {
            odb.ToTable("OrdersDetail");
            odb.HasKey(od => od.Id); { odb.Property(od => od.Id).ValueGeneratedOnAdd(); }
            odb.Property(od => od.ProductId);
            odb.Property(od => od.Price).HasColumnType("decimal(18,2)");
            odb.Property(od => od.Quantity).HasColumnType("unsigned int");
            odb.Property(od => od.SubTotalPrice).HasColumnType("decimal(18,2)");
        });
    }
}