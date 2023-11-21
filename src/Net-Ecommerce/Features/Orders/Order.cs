using Net_Ecommerce.Features.Sellers;
using Net_Ecommerce.Features.Users;

namespace Net_Ecommerce.Features.Orders;

public partial class Order
{
    public Order(Guid userId, string username, string email, Guid sellerId, OrderDetail item)
    {
        UserId = userId;
        Username = username;
        Email = email;
        SellerId = sellerId;
        Items = new List<OrderDetail>() { item };
    }

    public Order(Guid userId, string username, string email, Guid sellerId, IEnumerable<OrderDetail> items)
    {
        UserId = userId;
        Username = username;
        Email = email;
        SellerId = sellerId;
        Items = items;
        TotalPrice = items.Sum(i => i.SubTotalPrice);
    }

    public Guid Id { get; }
    public Guid UserId { get; }
    public string Username { get; }
    public string Email { get; }
    public Guid SellerId { get; }
    public IEnumerable<OrderDetail> Items { get; }
    public decimal TotalPrice { get; }

    // Navigation Props
    public User User { get; } = null!;
    public Seller Seller { get; } = null!;

#pragma warning disable 
    private Order()
    {
        
    }
#pragma warning restore

}