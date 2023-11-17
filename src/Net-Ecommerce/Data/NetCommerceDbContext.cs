using Microsoft.EntityFrameworkCore;
using Net_Ecommerce.Features.Products;
using Net_Ecommerce.Features.Sellers;
using Net_Ecommerce.Features.Users;

namespace Net_Ecommerce.Data;

public class NetCommerceDbContext : DbContext
{
    public NetCommerceDbContext(DbContextOptions<NetCommerceDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NetCommerceDbContext).Assembly);
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Seller> Sellers => Set<Seller>();
}