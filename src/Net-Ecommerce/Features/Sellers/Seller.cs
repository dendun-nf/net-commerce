using Net_Ecommerce.Features.Products;

namespace Net_Ecommerce.Features.Sellers;

public class Seller
{
    public Seller(string username, string email)
    {
        Username = username;
        Email = email;
    }

    public Guid Id { get; set; }
    public string Username { get; }
    public string Email { get; }

    private readonly List<Product> _products = new();
    public IEnumerable<Product> Products => _products.AsReadOnly();
    public void AddProduct(Product product) => _products.Add(product);
    public void AddProducts(IEnumerable<Product> products) => _products.AddRange(products);
    public void RemoveProduct(Guid id) 
    {
        var product = _products.First(p => p.Id == id)
            ?? throw new Exception("Product not found");
        _products.Remove(product);
    }
    public void UpdateProduct(Product product)
    {
        var target = _products.First(p => p.Id == product.Id)
            ?? throw new Exception("Product not found");
        target.ChangeComponents(
            product.Name,
            product.Description,
            product.Stock,
            product.Price);
    }

#pragma warning disable
    // For EF Core
    private Seller()
    {
        
    }   
#pragma warning restore
}