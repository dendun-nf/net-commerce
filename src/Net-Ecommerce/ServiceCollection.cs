using Microsoft.EntityFrameworkCore;
using Net_Ecommerce.Data;

namespace Net_Ecommerce;

public static class ServiceCollection
{
    public static IServiceCollection AddNetCommerceService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NetCommerceDbContext>(o => o.UseSqlite(configuration.GetConnectionString("NetCommerce")));
        return services;
    }
}