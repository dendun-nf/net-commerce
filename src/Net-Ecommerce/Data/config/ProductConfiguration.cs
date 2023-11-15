using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net_Ecommerce.Features.Products;

namespace Net_Ecommerce.Data.config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id); { builder.Property(p => p.Id).ValueGeneratedOnAdd(); }
        builder.Property(p => p.Name).HasColumnType("varchar(100)");
        builder.Property(p => p.Description).HasColumnType("varchar(1000)");
        builder.Property(p => p.Stock).HasColumnType("unsigned int");
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
    }
}