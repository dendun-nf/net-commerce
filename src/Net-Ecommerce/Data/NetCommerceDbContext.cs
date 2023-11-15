using Microsoft.EntityFrameworkCore;

namespace Net_Ecommerce.Data;

public class NetCommerceDbContext : DbContext
{
    public NetCommerceDbContext(DbContextOptions<NetCommerceDbContext> options) : base(options)
    {
    }
    
}