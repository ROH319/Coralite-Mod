using Coralite.Content.Items.Crimson;
using Coralite.Content.ModPlayers;
using Coralite.Core;
using Coralite.Core.Attributes;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace Coralite.Content.Items.Corruption
{
    [PlayerEffect]
    [AutoloadEquip(EquipType.Neck)]
    public class RottenAmulet : ModItem
    {
        public override string Texture => AssetDirectory.CorruptionItems + Name;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<BloodAmulet>();
        }

        public override void SetDefaults()
        {
            Item.SetShopValues(Terraria.Enums.ItemRarityColor.Orange3, Item.sellPrice(0, 2, 50));
            Item.defense = 3;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.TryGetModPlayer(out CoralitePlayer cp))
                cp.AddEffect(nameof(RottenAmulet));
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }
    }

    public class LimbRebirth : ModBuff
    {
        public override string Texture => AssetDirectory.CorruptionItems + Name;

        public LocalizedText Current => this.GetLocalization("Current");

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            if (Main.LocalPlayer.TryGetModPlayer(out RottenAmuletPlayer rap))
                tip += $"\n{Current.Value}{rap.limbRebirthCount}";
        }
    }

    public class RottenAmuletPlayer : ModPlayer
    {
        /// <summary>
        /// 受击次数
        /// </summary>
        public int limbRebirthCount;
        /// <summary>
        /// 腐烂护符回血效果CD
        /// </summary>
        public int limbRebirthCD;

        public override void OnHurt(Player.HurtInfo info)
        {
            if (!(Player.TryGetModPlayer(out CoralitePlayer cp) && cp.HasEffect(nameof(RottenAmulet)))
                || limbRebirthCD != 0 || info.Damage < 20)
                return;

            Player.AddBuff(ModContent.BuffType<LimbRebirth>(), 60 * 15);
            limbRebirthCount++;

            if (limbRebirthCount >= 6)
            {
                limbRebirthCount = 0;
                limbRebirthCD = 60 * 10;
                Player.Heal(Player.statLifeMax2 / 10);
                Player.ClearBuff(ModContent.BuffType<LimbRebirth>());
            }
        }

        public override void ResetEffects()
        {
            if (limbRebirthCD > 0)
                limbRebirthCD--;
        }

        public override void UpdateLifeRegen()
        {
            if (Player.HasBuff<LimbRebirth>())
                Player.lifeRegen += limbRebirthCount;
        }
    }
}
