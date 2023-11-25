using MediatR;
using Net_Ecommerce.Data;
using Net_Ecommerce.Features.Common;
using Net_Ecommerce.Features.Orders;

namespace Net_Ecommerce.Features.Users.GetOrders;

public class OrdersByUserHandler : IRequestHandler<OrdersByUserRequest, PagedList<OrderResponse>>
{
    private readonly NetCommerceDbContext _ctx;

    public OrdersByUserHandler(NetCommerceDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<PagedList<OrderResponse>> Handle(OrdersByUserRequest request, CancellationToken cancellationToken)
    {

        var ordersQuery = _ctx.Users.SelectMany(u => u.Orders)
            .Where(o => o.UserId == request.UserId)
            .AsQueryable();

        var ordersPage = await PagedList<Order>.CreateAsync(ordersQuery, request.PageNumber, request.PageSize);
        int ordersTotalCount = ordersPage.Count;

        var orderResponses = new List<OrderResponse>();

        foreach (var order in ordersPage.Items)
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

        return PagedList<OrderResponse>.Create(
            orderResponses,
            ordersTotalCount,
            request.PageNumber,
            request.PageSize);
    }
}