using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Net_Ecommerce.Features.Common;
using Net_Ecommerce.Features.Orders;

namespace Net_Ecommerce.Features.Users.GetOrders;

public record OrdersByUserRequest(
    [BindRequired] Guid UserId,
    int PageNumber = 1,
    int PageSize = 5) : IRequest<PagedList<OrderResponse>> 
{
}