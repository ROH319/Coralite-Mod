using Coralite.Core;
using Coralite.Core.Attributes;
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
            Item.SetShopValues(ItemRarityColor.LightRed4, Item.sellPrice(0, 1, 50));
            Item.defense = 26;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<B9Breastplate>() && legs.type == ItemType<B9Legs>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.05f;
            player.GetCritChance(DamageClass.Melee) += 5f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
        }

        public override void UpdateArmorSet(Player player)
        {
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
            Item.SetShopValues(ItemRarityColor.LightRed4, Item.sellPrice(0, 1, 50));
            Item.defense = 10;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<B9Breastplate>() && legs.type == ItemType<B9Legs>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.1f;
            player.GetCritChance(DamageClass.Ranged) += 6f;
        }

        public override void UpdateArmorSet(Player player)
        {
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
            Item.SetShopValues(ItemRarityColor.LightRed4, Item.sellPrice(0, 1, 50));
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<B9Breastplate>() && legs.type == ItemType<B9Legs>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.1f;
            player.GetCritChance(DamageClass.Magic) += 6f;
            player.statManaMax2 += 60;
        }

        public override void UpdateArmorSet(Player player)
        {
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
            Item.SetShopValues(ItemRarityColor.LightRed4, Item.sellPrice(0, 1, 50));
            Item.defense = 3;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<B9Breastplate>() && legs.type == ItemType<B9Legs>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.13f;
            player.whipRangeMultiplier += 0.1f;
            player.maxMinions += 1;
        }

        public override void UpdateArmorSet(Player player)
        {
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
            Item.SetShopValues(ItemRarityColor.LightRed4, Item.sellPrice(0, 1, 50));
            Item.defense = 3;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<B9Breastplate>() && legs.type == ItemType<B9Legs>();
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.13f;
            player.whipRangeMultiplier += 0.1f;
            player.maxMinions += 1;
        }

        public override void UpdateArmorSet(Player player)
        {
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
            Item.defense = 19;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.04f;
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
            Item.defense = 14;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.03f;
            player.moveSpeed += 0.05f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<B9Alloy>(16)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
