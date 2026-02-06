using Coralite.Core;
using Coralite.Helpers;
using Terraria;
using Terraria.ID;

namespace Coralite.Content.Items.Steel
{
    public class SteelAxe : ModItem
    {
        public override string Texture => AssetDirectory.SteelItems + Name;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.BonusAttackSpeedMultiplier[Type] = 0.25f;
        }

        public override void SetDefaults()
        {
            Item.DefaultToAxe(57, 8, 7f, 100);
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
