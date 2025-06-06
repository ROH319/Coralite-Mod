﻿using Coralite.Core;
using Terraria;
using Terraria.ID;

namespace Coralite.Content.Items.CoreKeeper
{
    public class AncientGemstone : ModItem
    {
        public override string Texture => AssetDirectory.CoreKeeperItems + Name;

        public override void SetStaticDefaults()
        {
            //ItemID.Sets.ShimmerTransformToItem[Type] = ItemID.GlowTulip;
            //ItemID.Sets.ShimmerTransformToItem[ItemID.GlowTulip] = Type;
        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 40;
            Item.maxStack = Item.CommonMaxStack;

            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ModContent.RarityType<RareRarity>();
        }

        public override void AddRecipes()
        {
            CreateRecipe(10)
                .AddCondition(CoraliteConditions.UseShlimmerTranslation)
                .AddCustomShimmerResult(ItemID.GlowTulip)
                .Register();

            Recipe.Create(ItemID.GlowTulip)
                .AddCondition(CoraliteConditions.UseShlimmerTranslation)
                .AddCustomShimmerResult(Type, 10)
                .Register();
        }
    }
}
