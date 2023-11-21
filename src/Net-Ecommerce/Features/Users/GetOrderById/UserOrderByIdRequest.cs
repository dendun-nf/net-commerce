using MediatR;
using Net_Ecommerce.Features.Orders;

namespace Net_Ecommerce.Features.Users.GetOrderById;

public record UserOrderByIdRequest(Guid UserId, Guid OrderId) : IRequest<OrderResponse>
{
    
}