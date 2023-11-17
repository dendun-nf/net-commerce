using MediatR;

namespace Net_Ecommerce.Features.Products.GetById;

public record ProductByIdRequest(Guid Id) : IRequest<ProductResponse>
{
    
}
