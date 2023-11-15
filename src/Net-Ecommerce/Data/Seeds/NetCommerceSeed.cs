using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Net_Ecommerce.Data.Seeds;

public static class NetCommerceSeed
{
    public static async Task Initialize(this NetCommerceDbContext ctx)
    {
        var migrationHistories = ctx.Database.GetService<IMigrationsAssembly>().Migrations.Select(m => m.Key);
        var appliedMigrations = ctx.Database.GetService<IHistoryRepository>().GetAppliedMigrations().Select(m => m.MigrationId);

        if(!migrationHistories.Except(appliedMigrations).Any())
        {
            await ctx.Database.MigrateAsync();
            await ctx.Seed();
        }
    }

    public static async Task Seed(this NetCommerceDbContext ctx)
    {
        await ctx.Users.AddRangeAsync(UserSeed.Users);
        await ctx.SaveChangesAsync();
    }
}