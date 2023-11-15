namespace Net_Ecommerce.Features.Users;

public class User
{
    public User(string username, string email)
    {
        Username = username;
        Email = email;
    }

    public Guid Id { get; }
    public string Username { get; }
    public string Email { get; }

#pragma warning disable
// For EF Core
    private User()
    {
        
    }
#pragma warning restore
}