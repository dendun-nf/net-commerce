using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net_Ecommerce.Features.Sellers;
using Net_Ecommerce.Features.Users;

namespace Net_Ecommerce.Data.config;

public class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.HasKey(s => s.Id); { builder.Property(s => s.Id).ValueGeneratedOnAdd(); }
        builder.Property(s => s.Username).HasColumnType("varchar(25)");
        builder.Property(s => s.Email).HasColumnType("varchar(100)");

        builder.HasMany(s => s.Products)
            .WithOne()
            .HasForeignKey(s => s.SellerId);

    }
}