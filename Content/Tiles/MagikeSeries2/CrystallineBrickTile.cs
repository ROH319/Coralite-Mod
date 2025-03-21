﻿using Coralite.Core;
using Terraria;
using Terraria.ID;

namespace Coralite.Content.Tiles.MagikeSeries2
{
    public class CrystallineBrickTile : ModTile
    {
        public override string Texture => AssetDirectory.MagikeSeries2Tile + Name;

        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.CanBeClearedDuringGeneration[Type] = false;
            TileID.Sets.CanBeClearedDuringOreRunner[Type] = false;

            MineResist = 3f;
            DustType = DustID.PurpleTorch;
            HitSound = CoraliteSoundID.DigStone_Tink;
            MinPick = 150;

            AddMapEntry(Coralite.CrystallineMagikePurple);
        }

        public override bool CanExplode(int i, int j) => false;
    }
}
