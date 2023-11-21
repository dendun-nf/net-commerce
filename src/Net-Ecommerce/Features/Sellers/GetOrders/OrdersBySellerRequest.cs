using MediatR;
using Net_Ecommerce.Features.Orders;

namespace Net_Ecommerce.Features.Sellers.GetOrders;

public record OrdersBySellerRequest(Guid SellerId) : IRequest<IEnumerable<OrderResponse>>
{
    
}