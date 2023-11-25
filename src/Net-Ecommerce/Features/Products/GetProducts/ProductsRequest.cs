using MediatR;
using Net_Ecommerce.Features.Common;

namespace Net_Ecommerce.Features.Products.GetProducts;

public record ProductsRequest(int PageNumber = 1, int PageSize = 5) : IRequest<PagedList<ProductResponse>>
{
    
}