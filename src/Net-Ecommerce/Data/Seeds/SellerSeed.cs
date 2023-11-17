using Microsoft.EntityFrameworkCore;
using Net_Ecommerce.Features.Sellers;

namespace Net_Ecommerce.Data.Seeds;

public static class SellerSeed
{
    
    public static List<Seller> Sellers = new()
    {
        new("Darrell39", "henhih@mom.tj"),
        new("Benjamin35", "hak@lig.af"),
        new("Rosa93", "bujci@veinubo.sh"),
        new("Olive48", "fapfe@hi.so"),
        new("Matilda7", "sas@wevenbed.gp"),
        new("Mason100", "laf@supu.eg"),
        new("Ricardo39", "doluptan@litikef.tg"),
        new("Linnie44", "babjobek@wacu.mk"),
        new("Leonard73", "mipu@wefuepo.nr"),
        new("Norman90", "ganagif@zodomte.cy"),
        new("Lloyd35", "uteozi@madcu.cu"),
        new("Winnie41", "eg@me.zm"),
        new("Mildred72", "to@oluta.kh"),
    };

    public static async Task AssignProductsToSellers(NetCommerceDbContext ctx)
    {
        var sellers = await ctx.Sellers.ToListAsync();
        foreach (var seller in sellers)
        {
            seller.AddProducts(ProductSeed.Products);
        }
    }
}