using Coralite.Core;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using static Terraria.ModLoader.ModContent;

namespace Coralite.Content.Items.Steel
{
    /// <summary>
    /// 战士头
    /// </summary>
    [AutoloadEquip(EquipType.Head)]
    public class SteelHelmet : ModItem
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
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<SteelBreastplate>() && legs.type == ItemType<SteelLegs>();
        }

        public override void UpdateEquip(Player player)
        {
        }

        public override void UpdateArmorSet(Player player)
        {
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SteelBar>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    /// <summary>
    /// 射手头
    /// </summary>
    [AutoloadEquip(EquipType.Head)]
    public class SteelCanHead : ModItem
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
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<SteelBreastplate>() && legs.type == ItemType<SteelLegs>();
        }

        public override void UpdateEquip(Player player)
        {
        }

        public override void UpdateArmorSet(Player player)
        {
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SteelBar>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    /// <summary>
    /// 法师头
    /// </summary>
    [AutoloadEquip(EquipType.Head)]
    public class SteelMask : ModItem
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
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<SteelBreastplate>() && legs.type == ItemType<SteelLegs>();
        }

        public override void UpdateEquip(Player player)
        {
        }

        public override void UpdateArmorSet(Player player)
        {
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SteelBar>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    /// <summary>
    /// 召唤头
    /// </summary>
    [AutoloadEquip(EquipType.Head)]
    public class SteelBucketHead : ModItem
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
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 6;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemType<SteelBreastplate>() && legs.type == ItemType<SteelLegs>();
        }

        public override void UpdateEquip(Player player)
        {
        }

        public override void UpdateArmorSet(Player player)
        {
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SteelBar>()
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }

    [AutoloadEquip(EquipType.Body)]
    public class SteelBreastplate : ModItem
    {
        public override string Texture => AssetDirectory.SteelItems + Name;

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SteelBar>()
                .AddTile(TileID.Anvils)
                .Register();
        }
    }

    [AutoloadEquip(EquipType.Legs)]
    public class SteelLegs : ModItem
    {
        public override string Texture => AssetDirectory.SteelItems + Name;

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Blue;
            Item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<SteelBar>()
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
