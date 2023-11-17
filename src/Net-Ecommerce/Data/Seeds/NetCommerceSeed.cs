using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Net_Ecommerce.Features.Sellers;
using Net_Ecommerce.Features.Users;

namespace Net_Ecommerce.Data.Seeds;


// NEED REFACTOR DEFINETLY
public static class NetCommerceSeed
{
    public static async Task Initialize(this NetCommerceDbContext ctx)
    {
        var migrationHistories = ctx.Database.GetService<IMigrationsAssembly>().Migrations.Select(m => m.Key);
        var appliedMigrations = ctx.Database.GetService<IHistoryRepository>().GetAppliedMigrations().Select(m => m.MigrationId);

        if(migrationHistories.Except(appliedMigrations).Any())
        {
            await ctx.Database.MigrateAsync();
        }
    }

    public static async Task Seed(this NetCommerceDbContext ctx)
    {
        var tablesNames = ctx.Model.GetEntityTypes().Select(e => e.GetTableName()).Distinct().ToList();
        foreach (var table in tablesNames)
        {
            switch (table)
            {
                case "Users":
                    await SeedUserIfNone(ctx);
                    break;
                case "Products":
                    await SeedProducts(ctx);
                    break;
                case "Sellers":
                    await SeedSellerIfNone(ctx);
                    break;
                default:
                    throw new Exception("Tables didn't exist");
            }   
        }
        
        await ctx.SaveChangesAsync();
    }

    private static async Task SeedSellerIfNone(NetCommerceDbContext ctx)
    {
        // if none on db and no local changed on ef core tracking
        if(!await ctx.Set<Seller>().AnyAsync() && ctx.Set<Seller>().Local.Count == 0)
            await ctx.Set<Seller>().AddRangeAsync(SellerSeed.Sellers);
    }

    private static async Task SeedUserIfNone(NetCommerceDbContext ctx)
    {
        if(!await ctx.Set<User>().AnyAsync())
            await ctx.Set<User>().AddRangeAsync(UserSeed.Users);
    }

    private static async Task SeedProducts(NetCommerceDbContext ctx)
    {
        var sellers = ctx.Set<Seller>().Local;
        if(!sellers.Any()) await SeedSellerIfNone(ctx);

        foreach (var seller in sellers)
        {
            seller.AddProducts(ProductSeed.Products);
        }
    }

    
}