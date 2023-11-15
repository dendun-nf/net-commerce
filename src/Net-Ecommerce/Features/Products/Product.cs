namespace Net_Ecommerce.Features.Products;

public class Product
{
    public Product(string name, string description, int stock, decimal price)
    {
        Name = name;
        Description = description;
        Stock = stock;
        Price = price;
    }

    public Guid Id { get; set; }
    public string Name { get; }
    public string Description { get; }
    public int Stock { get; }
    public decimal Price { get; }
}