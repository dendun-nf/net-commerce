using Net_Ecommerce.Features.Products;

namespace Net_Ecommerce.Features.Sellers.GetById;

public record SellerResponse(string Username, IEnumerable<ProductResponse> Products)
{
}