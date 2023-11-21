using MediatR;
using Net_Ecommerce.Features.Orders;

namespace Net_Ecommerce.Features.Sellers.GetOrderById;

public record SellerOrderByIdRequest(Guid SellerId, Guid OrderId) : IRequest<OrderResponse>
{
    
}