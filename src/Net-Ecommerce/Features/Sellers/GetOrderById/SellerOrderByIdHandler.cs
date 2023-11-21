using MediatR;
using Microsoft.EntityFrameworkCore;
using Net_Ecommerce.Data;
using Net_Ecommerce.Features.Orders;

namespace Net_Ecommerce.Features.Sellers.GetOrderById;

public class SellerOrderByIdHandler : IRequestHandler<SellerOrderByIdRequest, OrderResponse>
{
    private readonly NetCommerceDbContext _ctx;

    public SellerOrderByIdHandler(NetCommerceDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<OrderResponse> Handle(SellerOrderByIdRequest request, CancellationToken cancellationToken)
    {
        var order = await _ctx.Sellers.SelectMany(s => s.Orders)
            .FirstOrDefaultAsync(s => s.SellerId == request.SellerId
                && s.Id == request.OrderId) ?? throw new Exception("order not found");

        List<OrderDetailResponse> orderDetailResponses = new(); 
        foreach (var item in order.Items)
        {
            orderDetailResponses.Add(new OrderDetailResponse(
                item.ProductId,
                item.Price,
                item.Quantity,
                item.SubTotalPrice));
        }

        var orderResponse = new OrderResponse(
            order.Id,
            order.SellerId,
            order.UserId,
            order.Username,
            orderDetailResponses,
            order.TotalPrice);

        return orderResponse;
    }
}