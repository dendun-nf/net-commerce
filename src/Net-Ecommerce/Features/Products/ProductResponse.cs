namespace Net_Ecommerce.Features.Products;
public record ProductResponse(Guid Id, Guid SellerId, string Name, string Description, int Stock, decimal Price)
{
}