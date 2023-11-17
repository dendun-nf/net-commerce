using MediatR;
using Microsoft.EntityFrameworkCore;
using Net_Ecommerce.Data;
using Net_Ecommerce.Features.Products;

namespace Net_Ecommerce.Features.Sellers.GetById;

public class SellerByIdHandler : IRequestHandler<SellerByIdRequest, SellerResponse>
{
    private readonly NetCommerceDbContext _ctx;

    public SellerByIdHandler(NetCommerceDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<SellerResponse> Handle(SellerByIdRequest request, CancellationToken cancellationToken)
    {
        var seller = await _ctx.Sellers
            .Include(s => s.Products)
            .SingleAsync(s => s.Id == request.Id, cancellationToken);


        // need mapper
        List<ProductResponse> productsResponse = new();
        foreach (var product in seller.Products)
        {
            productsResponse.Add(new(product.Id, product.SellerId, product.Name, product.Description, product.Stock, product.Price));
        }

        return new SellerResponse(seller.Username, productsResponse);
    }
}