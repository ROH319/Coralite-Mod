using Coralite.Core;
using Terraria;
using Terraria.ID;

namespace Coralite.Content.Items.Crimson
{
    public class VertebraeKey : ModItem
    {
        public override string Texture => AssetDirectory.CrimsonItems + Name;

        public override void SetDefaults()
        {
            Item.maxStack = 99;
            Item.SetShopValues(Terraria.Enums.ItemRarityColor.Blue1, Item.sellPrice(0, 0, 50));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TissueSample, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
