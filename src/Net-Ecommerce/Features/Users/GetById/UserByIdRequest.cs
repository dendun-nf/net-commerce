using MediatR;

namespace Net_Ecommerce.Features.Users.GetById;

public record UserByIdRequest(Guid Id)
    : IRequest<UserResponse>
{
    
}