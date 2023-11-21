using Net_Ecommerce.Features.Sellers;

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

    public Guid Id { get; }
    public Guid SellerId { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Stock { get; private set; }
    public decimal Price { get; private set; }

    internal void ChangeComponents(string name, string description, int stock, decimal price)
    {
        Name = name;
        Description = description;
        Stock = stock;
        Price = price;
    }

    public void DecresaseStock(int num) => Stock -= num;
    public void IncreaseStock(int num) => Stock += num;


#pragma warning disable
    private Product()
    {
        
    }
#pragma warning restore
}