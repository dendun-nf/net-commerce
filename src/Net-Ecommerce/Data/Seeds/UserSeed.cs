using Microsoft.EntityFrameworkCore;
using Net_Ecommerce.Features.Users;

namespace Net_Ecommerce.Data.Seeds;

public static class UserSeed
{
    private static IReadOnlyCollection<User> Users => new List<User>()
    {
        new User("admin", "admin@localhost"),
        new User("test", "test@localhost")
    };

    public static async Task Seed(NetCommerceDbContext ctx)
    {
        if(!await ctx.Set<User>().AnyAsync() && ctx.Set<User>().Local.Count <= 0)
            await ctx.Set<User>().AddRangeAsync(Users);
    }
}