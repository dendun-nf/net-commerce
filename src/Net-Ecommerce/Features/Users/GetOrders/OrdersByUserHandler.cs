using MediatR;
using Microsoft.EntityFrameworkCore;
using Net_Ecommerce.Data;
using Net_Ecommerce.Features.Orders;

namespace Net_Ecommerce.Features.Users.GetOrders;

public class OrdersByUserHandler : IRequestHandler<OrdersByUserRequest, IEnumerable<OrderResponse>>
{
    private readonly NetCommerceDbContext _ctx;

    public OrdersByUserHandler(NetCommerceDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<OrderResponse>> Handle(OrdersByUserRequest request, CancellationToken cancellationToken)
    {
        var orders = await _ctx.Users
            .AsNoTracking()
            .SelectMany(u => u.Orders)
            .Where(o => o.UserId == request.UserId)
            .ToListAsync();

        var orderResponses = new List<OrderResponse>();

        foreach (var order in orders)
        {
            var orderDetailResponse = new List<OrderDetailResponse>();
            foreach (var item in order.Items)
            {
                orderDetailResponse.Add(new OrderDetailResponse(
                    item.ProductId,
                    item.Price,
                    item.Quantity,
                    item.SubTotalPrice));
            }
            orderResponses.Add(new OrderResponse(
                order.Id,
                order.SellerId,
                order.UserId,
                order.Username,
                orderDetailResponse,
                order.TotalPrice));
        }

        return orderResponses;
    }
}