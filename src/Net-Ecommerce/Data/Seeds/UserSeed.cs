using Net_Ecommerce.Features.Users;

namespace Net_Ecommerce.Data.Seeds;

public static class UserSeed
{
    public static IReadOnlyCollection<User> Users => new List<User>()
    {
        new User("admin", "admin@localhost"),
        new User("test", "test@localhost")
    };
}