using MediatR;
using Net_Ecommerce.Data;
using Net_Ecommerce.Features.Common;
using Net_Ecommerce.Features.Orders;

namespace Net_Ecommerce.Features.Sellers.GetOrders;

public class OrdersBySellerHandler : IRequestHandler<OrdersBySellerRequest, PagedList<OrderResponse>>
{
    private readonly NetCommerceDbContext _ctx;

    public OrdersBySellerHandler(NetCommerceDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<PagedList<OrderResponse>> Handle(OrdersBySellerRequest request, CancellationToken cancellationToken)
    {

        var ordersQuery = _ctx.Sellers
            .Where(s => s.Id == request.SellerId)
            .SelectMany(s => 
            s.Orders).AsQueryable();

        var ordersPage = await PagedList<Order>.CreateAsync(ordersQuery, request.PageNumber, request.PageSize);

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
            ordersPage.Count,
            request.PageNumber,
            request.PageSize);
        
    }
}