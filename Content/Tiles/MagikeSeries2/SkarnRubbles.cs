﻿using Coralite.Content.Dusts;
using Coralite.Content.Items.MagikeSeries2;
using Coralite.Core;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ObjectData;

namespace Coralite.Content.Tiles.MagikeSeries2
{
    public class SkarnRubbles1x1 : ModTile
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + Name;

        public const int Random = 9;

        public override void SetStaticDefaults()
        {
            DefaultValues(true);
        }

        protected void DefaultValues(bool UseRandom)
        {
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileID.Sets.CanBeClearedDuringGeneration[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.CoordinateHeights = [26];
            TileObjectData.newTile.DrawYOffset = -8;
            TileObjectData.newTile.StyleMultiplier = 1;
            TileObjectData.newTile.StyleHorizontal = true;
            if (UseRandom)
                TileObjectData.newTile.RandomStyleRange = Random;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorInvalidTiles = [TileID.Cloud, ModContent.TileType<SkarnBrickTile>(), ModContent.TileType<CrystallineBrickTile>(), ModContent.TileType<CrystallineBlockTile>()];
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = ModContent.DustType<SkarnDust>();
            AddMapEntry(new Color(141, 171, 178));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }

    public class SkarnRubbles1x1Fake : SkarnRubbles1x1
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + nameof(SkarnRubbles1x1);

        public override void SetStaticDefaults()
        {
            DefaultValues(false);
            FlexibleTileWand.RubblePlacementSmall.AddVariations(ModContent.ItemType<Skarn>(), Type, 0, 1, 2, 3, 4, 5, 6, 7, 8);
            RegisterItemDrop(ModContent.ItemType<Skarn>());
        }
    }

    public class SkarnRubbles2x1 : ModTile
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + Name;

        public const int Random = 8;

        public override void SetStaticDefaults()
        {
            DefaultValues(true);
        }

        protected void DefaultValues(bool UseRandom)
        {
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileID.Sets.CanBeClearedDuringGeneration[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x1);
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.StyleMultiplier = 1;
            TileObjectData.newTile.StyleHorizontal = true;
            if (UseRandom)
                TileObjectData.newTile.RandomStyleRange = Random;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorInvalidTiles = [TileID.Cloud, ModContent.TileType<SkarnBrickTile>(), ModContent.TileType<CrystallineBrickTile>(), ModContent.TileType<CrystallineBlockTile>()];
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = ModContent.DustType<SkarnDust>();
            AddMapEntry(new Color(141, 171, 178));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }

    public class SkarnRubbles2x1Fake : SkarnRubbles2x1
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + nameof(SkarnRubbles2x1);

        public override void SetStaticDefaults()
        {
            DefaultValues(false);

            FlexibleTileWand.RubblePlacementMedium.AddVariations(ModContent.ItemType<Skarn>(), Type, 0, 1, 2, 3, 4, 5, 6, 7);
            RegisterItemDrop(ModContent.ItemType<Skarn>());
        }
    }

    public class SkarnRubbles2x2 : ModTile
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + Name;

        public const int Random = 8;

        public override void SetStaticDefaults()
        {
            DefaultValues(true);
        }

        protected void DefaultValues(bool UseRandom)
        {
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileID.Sets.CanBeClearedDuringGeneration[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.StyleMultiplier = 1;
            TileObjectData.newTile.StyleHorizontal = true;
            if (UseRandom)
                TileObjectData.newTile.RandomStyleRange = Random;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorInvalidTiles = [TileID.Cloud, ModContent.TileType<SkarnBrickTile>(), ModContent.TileType<CrystallineBrickTile>(), ModContent.TileType<CrystallineBlockTile>()];
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = ModContent.DustType<SkarnDust>();
            AddMapEntry(new Color(141, 171, 178));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }

    public class SkarnRubbles2x2Fake : SkarnRubbles2x2
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + nameof(SkarnRubbles2x2);

        public override void SetStaticDefaults()
        {
            DefaultValues(false);

            FlexibleTileWand.RubblePlacementMedium.AddVariations(ModContent.ItemType<Skarn>(), Type, 0, 1, 2, 3, 4, 5, 6, 7);
            RegisterItemDrop(ModContent.ItemType<Skarn>());
        }
    }

    public class SkarnRubbles3x2 : ModTile
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + Name;

        public const int Random = 2;

        public override void SetStaticDefaults()
        {
            DefaultValues(true);
        }

        protected void DefaultValues(bool UseRandom)
        {
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileID.Sets.CanBeClearedDuringGeneration[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.StyleMultiplier = 1;
            TileObjectData.newTile.StyleHorizontal = true;
            if (UseRandom)
                TileObjectData.newTile.RandomStyleRange = Random;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorInvalidTiles = [TileID.Cloud, ModContent.TileType<SkarnBrickTile>(), ModContent.TileType<CrystallineBrickTile>(), ModContent.TileType<CrystallineBlockTile>()];
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = ModContent.DustType<SkarnDust>();
            AddMapEntry(new Color(141, 171, 178));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }

    public class SkarnRubbles3x2Fake : SkarnRubbles3x2
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + nameof(SkarnRubbles3x2);

        public override void SetStaticDefaults()
        {
            DefaultValues(false);

            FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<Skarn>(), Type, 0, 1);
            RegisterItemDrop(ModContent.ItemType<Skarn>());
        }
    }

    public class SkarnRubbles3x3 : ModTile
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + Name;

        public const int Random = 4;

        public override void SetStaticDefaults()
        {
            DefaultValues(true);
        }

        protected void DefaultValues(bool UseRandom)
        {
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileID.Sets.CanBeClearedDuringGeneration[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.StyleMultiplier = 1;
            TileObjectData.newTile.StyleHorizontal = true;
            if (UseRandom)
                TileObjectData.newTile.RandomStyleRange = Random;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorInvalidTiles = [TileID.Cloud, ModContent.TileType<SkarnBrickTile>(), ModContent.TileType<CrystallineBrickTile>(), ModContent.TileType<CrystallineBlockTile>()];
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = ModContent.DustType<SkarnDust>();
            AddMapEntry(new Color(141, 171, 178));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }

    public class SkarnRubbles3x3Fake : SkarnRubbles3x3
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + nameof(SkarnRubbles3x3);

        public override void SetStaticDefaults()
        {
            DefaultValues(false);

            FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<Skarn>(), Type, 0, 1, 2, 3);
            RegisterItemDrop(ModContent.ItemType<Skarn>());
        }
    }

    public class SkarnRubbles3x4 : ModTile
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + Name;

        public const int Random = 4;

        public override void SetStaticDefaults()
        {
            DefaultValues(true);
        }

        protected void DefaultValues(bool UseRandom)
        {
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileID.Sets.CanBeClearedDuringGeneration[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.StyleMultiplier = 1;
            TileObjectData.newTile.StyleHorizontal = true;
            if (UseRandom)
                TileObjectData.newTile.RandomStyleRange = Random;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorInvalidTiles = [TileID.Cloud, ModContent.TileType<SkarnBrickTile>(), ModContent.TileType<CrystallineBrickTile>(), ModContent.TileType<CrystallineBlockTile>()];
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = ModContent.DustType<SkarnDust>();
            AddMapEntry(new Color(141, 171, 178));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }

    public class SkarnRubbles3x4Fake : SkarnRubbles3x4
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + nameof(SkarnRubbles3x4);

        public override void SetStaticDefaults()
        {
            DefaultValues(false);

            FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<Skarn>(), Type, 0, 1, 2, 3);
            RegisterItemDrop(ModContent.ItemType<Skarn>());
        }
    }

    public class SkarnRubbles4x2 : ModTile
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + Name;

        public const int Random = 1;

        public override void SetStaticDefaults()
        {
            DefaultValues(true);
        }

        protected void DefaultValues(bool UseRandom)
        {
            Main.tileNoFail[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileID.Sets.CanBeClearedDuringGeneration[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.StyleMultiplier = 1;
            TileObjectData.newTile.StyleHorizontal = true;
            if (UseRandom)
                TileObjectData.newTile.RandomStyleRange = Random;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.newTile.AnchorInvalidTiles = [TileID.Cloud, ModContent.TileType<SkarnBrickTile>(), ModContent.TileType<CrystallineBrickTile>(), ModContent.TileType<CrystallineBlockTile>()];
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = ModContent.DustType<SkarnDust>();
            AddMapEntry(new Color(141, 171, 178));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }

    public class SkarnRubbles4x2Fake : SkarnRubbles4x2
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + nameof(SkarnRubbles4x2);

        public override void SetStaticDefaults()
        {
            DefaultValues(false);

            FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<Skarn>(), Type, 0);
            RegisterItemDrop(ModContent.ItemType<Skarn>());
        }
    }

    public class SkarnRubbles6x6 : ModTile
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + Name;

        public override void SetStaticDefaults()
        {
            DefaultValues();
            MinPick = 110;
        }

        protected void DefaultValues()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileObsidianKill[Type] = true;

            TileID.Sets.CanBeClearedDuringGeneration[Type] = false;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
            TileObjectData.newTile.Height = 6;
            TileObjectData.newTile.Width = 6;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 16, 16, 16];
            TileObjectData.newTile.Origin = new Terraria.DataStructures.Point16(0, 5);
            TileObjectData.newTile.DrawYOffset = 2;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.LavaDeath = false;
            TileObjectData.addTile(Type);

            HitSound = CoraliteSoundID.DigStone_Tink;
            DustType = ModContent.DustType<SkarnDust>();
            AddMapEntry(new Color(141, 171, 178));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }

    public class SkarnRubbles6x6Fake : SkarnRubbles6x6
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + nameof(SkarnRubbles6x6);

        public override void SetStaticDefaults()
        {
            DefaultValues();

            FlexibleTileWand.RubblePlacementLarge.AddVariations(ModContent.ItemType<Skarn>(), Type, 0);
            RegisterItemDrop(ModContent.ItemType<Skarn>());
        }
    }
}
