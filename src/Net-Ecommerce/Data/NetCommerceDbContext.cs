using Microsoft.EntityFrameworkCore;
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
}