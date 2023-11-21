namespace Net_Ecommerce.Features.Orders;

public static class OrderCalculator
{
    public static decimal Calculate(decimal price, int quantity)
    {
        return price * quantity;
    }
}