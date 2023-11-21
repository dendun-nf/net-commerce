using Net_Ecommerce.Features.Orders;

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

    private readonly List<Order> _orders = new();
    public IEnumerable<Order> Orders => _orders.AsReadOnly();

    public void AddOrder(Order order) => _orders.Add(order);
    public void AddOrders(IEnumerable<Order> orders) => _orders.AddRange(orders);
    
#pragma warning disable
// For EF Core
    private User()
    {
        
    }
#pragma warning restore
}