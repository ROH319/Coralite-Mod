using Coralite.Content.Items.Corruption;
using Coralite.Content.ModPlayers;
using Coralite.Core;
using Coralite.Core.Attributes;
using Terraria;
using Terraria.ID;
using Terraria.Localization;

namespace Coralite.Content.Items.Crimson
{
    [PlayerEffect]
    [AutoloadEquip(EquipType.Neck)]
    public class BloodAmulet : ModItem
    {
        public override string Texture => AssetDirectory.CrimsonItems + Name;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[Type] = ModContent.ItemType<RottenAmulet>();
        }

        public override void SetDefaults()
        {
            Item.SetShopValues(Terraria.Enums.ItemRarityColor.Orange3, Item.sellPrice(0, 2, 50));
            Item.defense = 1;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.TryGetModPlayer(out CoralitePlayer cp))
                cp.AddEffect(nameof(BloodAmulet));

            if (player.TryGetModPlayer(out BloodAmuletPlayer bap))
            {
                if (player.HasBuff<Bloodthirsty>())
                {
                    player.GetDamage(DamageClass.Generic) += bap.bloodthirstyCount * 0.015f;
                    player.GetCritChance(DamageClass.Generic) += bap.bloodthirstyCount / 2;
                    player.moveSpeed += bap.bloodthirstyCount * 0.015f;
                }
                else
                    bap.bloodthirstyCount = 0;
            }
        }

        public override void ArmorSetShadows(Player player)
        {
            if (player.HasBuff<Bloodthirsty>())
                player.armorEffectDrawShadow = true;
        }
    }

    public class Bloodthirsty : ModBuff
    {
        public override string Texture => AssetDirectory.CrimsonItems + Name;

        public LocalizedText Current => this.GetLocalization("Current");

        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
        }

        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            if (Main.LocalPlayer.TryGetModPlayer(out BloodAmuletPlayer bap))
                tip += $"\n{Current.Value}{bap.bloodthirstyCount}";
        }
    }

    public class BloodAmuletPlayer : ModPlayer
    {
        public int bloodthirstyCount;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!(Player.TryGetModPlayer(out CoralitePlayer cp) && cp.HasEffect(nameof(BloodAmulet)))
                || target.life - damageDone > 0 || target.lifeMax <= 5 || target.SpawnedFromStatue)
                return;

            Player.AddBuff(ModContent.BuffType<Bloodthirsty>(), 60 * 20);
            bloodthirstyCount++;

            for (int i = 0; i < 16; i++)
            {
                Dust d = Dust.NewDustDirect(Player.position, Player.width, Player.height, DustID.Blood, Scale: Main.rand.NextFloat(1, 2));
                d.velocity = (i * MathHelper.TwoPi / 16).ToRotationVector2() * Main.rand.NextFloat(1, 4);
            }
        }

        public override void ResetEffects()
        {
            if (bloodthirstyCount > 10)
                bloodthirstyCount = 10;
        }
    }
}
