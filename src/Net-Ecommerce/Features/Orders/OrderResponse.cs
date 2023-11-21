namespace Net_Ecommerce.Features.Orders;

public record OrderResponse(
    Guid OrderId,
    Guid SellerId,
    Guid CustomerId,
    string OrderByName,
    IEnumerable<OrderDetailResponse> OrderItems,
    decimal TotalPrice)
{
    
}

public record OrderDetailResponse(
    Guid ProductId,
    decimal Price,
    int Quantity,
    decimal SubTotalPrice)
{
}