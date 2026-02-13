using Coralite.Content.DamageClasses;
using Coralite.Content.ModPlayers;
using Coralite.Core;
using Coralite.Core.Attributes;
using Coralite.Helpers;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace Coralite.Content.Items.Steel
{
    /// <summary>
    /// 战士头
    /// </summary>
    [AutoloadEquip(EquipType.Head)]
    public class B9LaserMask : ModItem
    {
        public override string Texture => AssetDirectory.SteelItems + Name;

        public override void SetDefaults()
        {
            Item.SetShopValues(ItemRarityColor.LightRed4, Item.sellPrice(0, 3));
            Item.defense = 20;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<B9Breastplate>() && legs.type == ItemType<B9Legs>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.1f;
            player.GetCritChance(DamageClass.Melee) += 10f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = B9Breastplate.bonus.Value;
            B9Breastplate.B9ArmorSet(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<B9Alloy>(12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    /// <summary>
    /// 射手头
    /// </summary>
    [AutoloadEquip(EquipType.Head)]
    public class B9MonitorHead : ModItem
    {
        public override string Texture => AssetDirectory.SteelItems + Name;

        public override void SetDefaults()
        {
            Item.SetShopValues(ItemRarityColor.LightRed4, Item.sellPrice(0, 3));
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<B9Breastplate>() && legs.type == ItemType<B9Legs>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.19f;
            player.GetCritChance(DamageClass.Ranged) += 8f;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = B9Breastplate.bonus.Value;
            B9Breastplate.B9ArmorSet(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<B9Alloy>(12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    /// <summary>
    /// 法师头
    /// </summary>
    [AutoloadEquip(EquipType.Head)]
    public class B9SpaceHelmet : ModItem
    {
        public override string Texture => AssetDirectory.SteelItems + Name;

        public override void SetDefaults()
        {
            Item.SetShopValues(ItemRarityColor.LightRed4, Item.sellPrice(0, 3));
            Item.defense = 3;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<B9Breastplate>() && legs.type == ItemType<B9Legs>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.19f;
            player.GetCritChance(DamageClass.Magic) += 8f;
            player.statManaMax2 += 100;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = B9Breastplate.bonus.Value;
            B9Breastplate.B9ArmorSet(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<B9Alloy>(12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    /// <summary>
    /// 召唤头
    /// </summary>
    [AutoloadEquip(EquipType.Head)]
    public class B9PlaneHead : ModItem
    {
        public override string Texture => AssetDirectory.SteelItems + Name;

        public static LocalizedText bonus;

        public override void Load()
        {
            bonus = this.GetLocalization("ArmorBonus");
        }

        public override void Unload()
        {
            bonus = null;
        }

        public override void SetDefaults()
        {
            Item.SetShopValues(ItemRarityColor.LightRed4, Item.sellPrice(0, 3));
            Item.defense = 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<B9Breastplate>() && legs.type == ItemType<B9Legs>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.19f;
            player.whipRangeMultiplier += 0.1f;
            player.maxMinions += 1;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.maxMinions += 2;
            player.setBonus = bonus.Value;
            B9Breastplate.B9ArmorSet(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<B9Alloy>(12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    /// <summary>
    /// 仙灵头
    /// </summary>
    [AutoloadEquip(EquipType.Head)]
    public class B9BatteryHead : ModItem
    {
        public override string Texture => AssetDirectory.SteelItems + Name;

        public static LocalizedText bonus;

        public override void Load()
        {
            bonus = this.GetLocalization("ArmorBonus");
        }

        public override void Unload()
        {
            bonus = null;
        }

        public override void SetDefaults()
        {
            Item.SetShopValues(ItemRarityColor.LightRed4, Item.sellPrice(0, 3));
            Item.defense = 5;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<B9Breastplate>() && legs.type == ItemType<B9Legs>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage<FairyDamage>() += 0.19f;
            player.GetCritChance<FairyDamage>() += 8f;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = B9Breastplate.bonus.Value;
            B9Breastplate.B9ArmorSet(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<B9Alloy>(12)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    [PlayerEffect]
    [AutoloadEquip(EquipType.Body)]
    public class B9Breastplate : ModItem
    {
        public override string Texture => AssetDirectory.SteelItems + Name;

        public static LocalizedText bonus;
        public const int BonusAffectRadius = 16 * 8;

        public override void Load()
        {
            bonus = this.GetLocalization("ArmorBonus");
        }

        public override void Unload()
        {
            bonus = null;
        }

        public override void SetDefaults()
        {
            Item.SetShopValues(ItemRarityColor.LightRed4, Item.sellPrice(0, 2, 50));
            Item.defense = 13;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.06f;
            player.GetCritChance(DamageClass.Generic) += 5f;
        }

        public static void B9ArmorSet(Player player)
        {
            if (player.TryGetModPlayer(out CoralitePlayer cp))
                cp.AddEffect(nameof(B9Breastplate));

            if (player.Alives() && player.ownedProjectileCounts[ProjectileType<B9ArmorEffectProj>()]<1)
                Projectile.NewProjectile(player.GetSource_FromThis(), player.Center, Vector2.Zero, ProjectileType<B9ArmorEffectProj>(),0,0,player.whoAmI);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<B9Alloy>(22)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class B9Legs : ModItem
    {
        public override string Texture => AssetDirectory.SteelItems + Name;

        public override void SetDefaults()
        {
            Item.SetShopValues(ItemRarityColor.LightRed4, Item.sellPrice(0, 1, 50));
            Item.defense = 9;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.05f;
            player.GetCritChance(DamageClass.Generic) += 5f;
            player.moveSpeed += 0.06f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<B9Alloy>(16)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    public class B9ArmorEffectProj : ModProjectile
    {
        public override string Texture => AssetDirectory.SteelItems + Name;

        public Player Owner => Main.player[Projectile.owner];

        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.friendly = true;
        }

        public override bool? CanDamage() => false;
        public override bool? CanCutTiles() => false;
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) => false;
        public override bool ShouldUpdatePosition() => false;

        public override void AI()
        {
            if (!Owner.Alives())
            {
                Projectile.Kill();
                return;
            }

            if (Projectile.IsOwnedByLocalPlayer())//只有主人端正常搞搞
                Projectile.Center = Main.MouseWorld;
            else
                Projectile.Center = Owner.Center;

            Projectile.ai[0]++;
            if (Owner.ItemAnimationActive)
                Projectile.ai[0]++;
            if (Projectile.ai[0] > 60 * 2)
                Projectile.ai[0] = 0;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            if (!Projectile.IsOwnedByLocalPlayer() || Owner.HeldItem.damage <1)
                return false;

            Texture2D tex = Projectile.GetTexture();
            Vector2 center = Projectile.Center - Main.screenPosition;
            Color c = new Color(239, 58, 12);
            float mouseLength = 8;
            float mouseScale = 0.75f;

            float f = Projectile.ai[0] / (60 * 2);
            float angle = 0;

            if (!Owner.ItemAnimationActive)
            {
                c *= 0.5f;
                mouseLength *= 2;
                mouseScale = 1;
                angle = f * MathHelper.TwoPi;
            }

            //绘制在框框上
            Draw(center + new Vector2(-B9Breastplate.BonusAffectRadius), 0, 2, c);
            Draw(center + new Vector2(B9Breastplate.BonusAffectRadius, -B9Breastplate.BonusAffectRadius), MathHelper.PiOver2, 2, c);
            Draw(center + new Vector2(-B9Breastplate.BonusAffectRadius, B9Breastplate.BonusAffectRadius), -MathHelper.PiOver2, 2, c);
            Draw(center + new Vector2(B9Breastplate.BonusAffectRadius), MathHelper.Pi, 2, c);

            //绘制在鼠标旁边
            Draw(center + new Vector2(-mouseLength).RotatedBy(angle), MathHelper.Pi+angle, mouseScale, c);
            Draw(center + new Vector2(mouseLength, -mouseLength).RotatedBy(angle), -MathHelper.PiOver2 + angle, mouseScale, c);
            Draw(center + new Vector2(-mouseLength, mouseLength).RotatedBy(angle), MathHelper.PiOver2 + angle, mouseScale, c);
            Draw(center + new Vector2(mouseLength).RotatedBy(angle), angle, mouseScale, c);

            //绘制在中间扩散
            float length = Helper.BezierEase(f)* B9Breastplate.BonusAffectRadius;
            c *= f;
            float scale = 1f + 1f * f;
            Draw(center + new Vector2(-length), 0, scale, c);
            Draw(center + new Vector2(length, -length), MathHelper.PiOver2, scale, c);
            Draw(center + new Vector2(-length, length), -MathHelper.PiOver2, scale, c);
            Draw(center + new Vector2(length), MathHelper.Pi, scale, c);

            Rectangle r = Utils.CenteredRectangle(Main.MouseWorld, new Vector2(B9Breastplate.BonusAffectRadius * 2));

            foreach (var npc in Main.ActiveNPCs)
                if (npc.CanBeChasedBy() && r.Contains((int)npc.Center.X, (int)npc.Center.Y))
                    DrawTargetNPC(npc.Center - Main.screenPosition);

            void Draw(Vector2 pos, float rot, float scale, Color c)
            {
                Main.spriteBatch.Draw(tex, pos, null, c, rot, Vector2.Zero, scale, 0, 0);
            }

            void DrawTargetNPC(Vector2 pos)
            {
                Draw(pos + new Vector2(-mouseLength).RotatedBy(angle), MathHelper.Pi + angle, mouseScale, c);
                Draw(pos + new Vector2(mouseLength, -mouseLength).RotatedBy(angle), -MathHelper.PiOver2 + angle, mouseScale, c);
                Draw(pos + new Vector2(-mouseLength, mouseLength).RotatedBy(angle), MathHelper.PiOver2 + angle, mouseScale, c);
                Draw(pos + new Vector2(mouseLength).RotatedBy(angle), angle, mouseScale, c);
            }

            return false;
        }
    }
}
