using Coralite.Core;
using Coralite.Helpers;
using Terraria;
using Terraria.ID;

namespace Coralite.Content.Items.Steel
{
    public class SteelPickaxe : ModItem
    {
        public override string Texture => AssetDirectory.SteelItems + Name;

        public override void SetDefaults()
        {
            Item.DefaultToPickaxe(20, 7, 3.5f, 180,21);
            Item.SetShopValues(Terraria.Enums.ItemRarityColor.Orange3, Item.sellPrice(0, 1, 50));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SteelBar>(), 15)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
