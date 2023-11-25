using Microsoft.EntityFrameworkCore;
using Net_Ecommerce.Features.Orders;
using Net_Ecommerce.Features.Sellers;
using Net_Ecommerce.Features.Users;

namespace Net_Ecommerce.Data.Seeds;

public class OrderSeed
{
    private readonly static List<Order> orders = new List<Order>()
    {

    };

    private static Dictionary<Guid, IEnumerable<Order>> GetOrdersForEachUser(IEnumerable<User> users, IEnumerable<Seller> sellers)
    {
        var random = new Random();
        var orderDict = new Dictionary<Guid, IEnumerable<Order>>();
        
        foreach (var user in users)
        {
            List<Order> orders = new();
            foreach (var seller in sellers)
            {
                List<OrderDetail> items = new();
                foreach (var product in seller.Products)
                {
                    int quantity = random.Next(1, product.Stock - 1);
                    items.Add(new(product.Id, product.Price, quantity));
                    product.DecresaseStock(quantity);
                }

                orders.Add(new Order(user.Id, user.Username, user.Email, seller.Id, items));
            }

            orderDict.Add(user.Id, orders);
        }

        return orderDict ;
    }

    public static async Task Seed(NetCommerceDbContext ctx)
    {
        if(await ctx.Orders.AnyAsync() || ctx.Orders.Local.Count > 0)
            return;

        // if none on db and none on local user
        if(!await ctx.Users.AnyAsync() && ctx.Users.Local.Count <= 0)
            await UserSeed.Seed(ctx);

        // if none on db and none on local products
        if(!await ctx.Products.AnyAsync() && ctx.Products.Local.Count <= 0)
            await ProductSeed.Seed(ctx);


        var users = await ctx.Users.ToListAsync();
        users = users.Count > 0 ? users : ctx.Users.Local.ToList();

        var sellers = await ctx.Sellers
            .Include(s => s.Products)
            .ToListAsync();
        sellers = sellers.Count > 0 ? sellers : ctx.Sellers.Local.ToList();
        
        var orderDict = GetOrdersForEachUser(users, sellers);

        foreach (var user in users)
        {
            user.AddOrders(orderDict[user.Id]);
        }
        
    }
}

// Delete THIS

// // each seller we iterate through
//         foreach (var seller in sellers)
//         {
//             var orders = new List<Order>();
//             var orderDetails = new List<OrderDetail>();

//             // each product of seller we make the order details
//             foreach (var product in seller.Products)
//             {
//                 var quantity = random.Next(1, product.Stock - 1);
//                 orderDetails.Add(new OrderDetail(product.Id, product.Price, quantity));
//                 product.DecresaseStock(quantity);
//             }

//             // each user we assign the orders made to the user
//             foreach (var user in users)
//             {
//                 orders.Add(new Order(user.Id, user.Username, user.Email, seller.Id, orderDetails));
//                 user.AddOrders(orders);
//             }
//         }

//         // foreach (var user in userIds)
//         // {
//         //     foreach (var seller in sellers)
//         //     {
                
//         //     }
//         // }