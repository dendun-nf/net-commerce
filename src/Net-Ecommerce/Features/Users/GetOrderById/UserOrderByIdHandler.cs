using MediatR;
using Microsoft.EntityFrameworkCore;
using Net_Ecommerce.Data;
using Net_Ecommerce.Features.Orders;

namespace Net_Ecommerce.Features.Users.GetOrderById;

public class UserOrderByIdHandler : IRequestHandler<UserOrderByIdRequest, OrderResponse>
{
    private readonly NetCommerceDbContext _ctx;

    public UserOrderByIdHandler(NetCommerceDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<OrderResponse> Handle(UserOrderByIdRequest request, CancellationToken cancellationToken)
    {
        var order = await _ctx.Users
            .AsNoTracking()
            .SelectMany(u => u.Orders)
            .FirstOrDefaultAsync(o => o.Id == request.OrderId && o.UserId == request.UserId)
            ?? throw new Exception("order not found");

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