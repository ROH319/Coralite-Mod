using Coralite.Content.GlobalItems;
using Coralite.Core;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace Coralite.Content.Items.Icicle
{
    public class IcicleStaff : ModItem
    {
        public override string Texture => AssetDirectory.IcicleItems + Name;

        public override void SetDefaults()
        {
            Item.DefaultToMagicWeapon(ProjectileType<IcicleStaffHeldProj>(), 14, 0, true);
            Item.SetWeaponValues(29, 3f);
            Item.mana = 16;

            Item.useStyle = ItemUseStyleID.Rapier;
            Item.SetShopValues(Terraria.Enums.ItemRarityColor.Green2, Item.sellPrice(0, 1));

            Item.useTurn = false;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            CoraliteGlobalItem.SetColdDamage(Item);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, player.Center, Vector2.Zero, type, damage, knockback, player.whoAmI);
            Helpers.Helper.PlayPitched("Icicle/IcicleStaff", 0.2f, 0f, player.Center);
            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient<IcicleCrystal>()
            .AddIngredient<IcicleBreath>(3)
            .AddTile(TileID.IceMachine)
            .Register();
        }
    }
}
