namespace Net_Ecommerce.Features.Orders;

public class OrderDetail
{
    public OrderDetail(Guid productId, decimal price, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
        SubTotalPrice = OrderCalculator.Calculate(price, quantity);
    }

    public int Id { get; }
    public Guid ProductId { get; }
    public int Quantity { get; }
    public decimal Price { get; set; }
    public decimal SubTotalPrice { get; } 

    // navigation prop
    public Order Order { get; } = null!;
}