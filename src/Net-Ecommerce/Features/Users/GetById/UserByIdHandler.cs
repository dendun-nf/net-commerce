using MediatR;
using Microsoft.EntityFrameworkCore;
using Net_Ecommerce.Data;

namespace Net_Ecommerce.Features.Users.GetById;

public class UserByIdHandler
    :IRequestHandler<UserByIdRequest, UserResponse>
{
    private readonly NetCommerceDbContext _ctx;

    public UserByIdHandler(NetCommerceDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<UserResponse> Handle(UserByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await _ctx.Users.SingleAsync(u => u.Id == request.Id, cancellationToken);

        return new(user.Username, user.Email);
    }
}