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
                    await UserSeed.Seed(ctx);
                    break;
                case "Products":
                    await ProductSeed.Seed(ctx);
                    break;
                case "Sellers":
                    await SellerSeed.Seed(ctx);
                    break;
                default:
                    continue;
            }   
        }
        
        await ctx.SaveChangesAsync();
    }

    
}