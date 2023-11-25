using MediatR;
using Net_Ecommerce.Data;
using Net_Ecommerce.Features.Common;

namespace Net_Ecommerce.Features.Products.GetProducts;

public class ProductsHandler : IRequestHandler<ProductsRequest, PagedList<ProductResponse>>
{
    private readonly NetCommerceDbContext _ctx;

    public ProductsHandler(NetCommerceDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<PagedList<ProductResponse>> Handle(ProductsRequest request, CancellationToken cancellationToken)
    {
        var productsQuery = _ctx.Products.AsQueryable();

        var productsResponses = productsQuery
            .Select(p => new ProductResponse(
                p.Id,
                p.SellerId,
                p.Name,
                p.Description,
                p.Stock,
                p.Price));

        var products = await PagedList<ProductResponse>.CreateAsync(
            productsResponses,
            request.PageNumber,
            request.PageSize);

        return products;
    }
}