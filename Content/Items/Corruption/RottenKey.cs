using Coralite.Core;
using Terraria;
using Terraria.ID;

namespace Coralite.Content.Items.Corruption
{
    public class RottenKey : ModItem
    {
        public override string Texture => AssetDirectory.CorruptionItems + Name;

        public override void SetDefaults()
        {
            Item.maxStack = 99;
            Item.SetShopValues(Terraria.Enums.ItemRarityColor.Blue1, Item.sellPrice(0, 0, 50));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.ShadowScale, 8)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
