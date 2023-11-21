using MediatR;
using Microsoft.EntityFrameworkCore;
using Net_Ecommerce.Data;
using Net_Ecommerce.Features.Orders;

namespace Net_Ecommerce.Features.Sellers.GetOrders;

public class OrdersBySellerHandler : IRequestHandler<OrdersBySellerRequest, IEnumerable<OrderResponse>>
{
    private readonly NetCommerceDbContext _ctx;

    public OrdersBySellerHandler(NetCommerceDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<OrderResponse>> Handle(OrdersBySellerRequest request, CancellationToken cancellationToken)
    {
        var orders = await _ctx.Sellers.SelectMany(s => s.Orders).Where(o => o.SellerId == request.SellerId).ToListAsync();

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