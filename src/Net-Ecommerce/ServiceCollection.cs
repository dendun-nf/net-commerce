using Microsoft.EntityFrameworkCore;
using Net_Ecommerce.Data;
using Net_Ecommerce.Features.Users.GetById;

namespace Net_Ecommerce;

public static class ServiceCollection
{
    public static IServiceCollection AddNetCommerceService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NetCommerceDbContext>(o => o.UseSqlite(configuration.GetConnectionString("NetCommerce")));
        services.AddMediatR(conf => 
        {
           conf.RegisterServicesFromAssemblyContaining<Program>(); 
        });

        services.AddScoped<UserByIdHandler>();
        return services;
    }
}