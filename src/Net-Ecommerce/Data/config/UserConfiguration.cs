using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Net_Ecommerce.Features.Users;

namespace Net_Ecommerce.Data.config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id); {builder.Property(u => u.Id).ValueGeneratedOnAdd(); }
        builder.Property(u => u.Username).HasColumnType("varchar(25)");
        builder.Property(u => u.Email).HasColumnType("varchar(100)");
    }
}