using MediatR;

namespace Net_Ecommerce.Features.Sellers.GetById;

public record SellerByIdRequest(Guid Id) : IRequest<SellerResponse>
{
    
}
