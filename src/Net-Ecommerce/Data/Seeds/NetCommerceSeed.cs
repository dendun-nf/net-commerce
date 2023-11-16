using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Net_Ecommerce.Features.Products;
using Net_Ecommerce.Features.Users;

namespace Net_Ecommerce.Data.Seeds;

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
                    if(!await ctx.Set<User>().AnyAsync())
                        await ctx.Set<User>().AddRangeAsync(UserSeed.Users);
                    break;
                case "Products":
                    if(!await ctx.Set<Product>().AnyAsync())
                        await ctx.Set<Product>().AddRangeAsync(ProductSeed.Products);
                    break;
                default:
                    throw new Exception("Tables didn't exist");
            }   
        }
        
        await ctx.SaveChangesAsync();
    }
}