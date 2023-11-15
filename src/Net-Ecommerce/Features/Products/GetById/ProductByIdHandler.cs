using MediatR;
using Microsoft.EntityFrameworkCore;
using Net_Ecommerce.Data;

namespace Net_Ecommerce.Features.Products.GetById;

public class ProductByIdHandler : IRequestHandler<ProductByIdRequest, ProductResponse>
{
    private readonly NetCommerceDbContext _ctx;

    public ProductByIdHandler(NetCommerceDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<ProductResponse> Handle(ProductByIdRequest request, CancellationToken cancellationToken)
    {
        var product = await _ctx.Products.SingleAsync(p => p.Id == request.Id);

        return new(product.Name, product.Description, product.Stock, product.Price);
    }
}