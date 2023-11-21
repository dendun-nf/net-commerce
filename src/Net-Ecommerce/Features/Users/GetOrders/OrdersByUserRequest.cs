using MediatR;
using Net_Ecommerce.Features.Orders;

namespace Net_Ecommerce.Features.Users.GetOrders;

public record OrdersByUserRequest(Guid UserId) : IRequest<IEnumerable<OrderResponse>> 
{
    
}