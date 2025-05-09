using CotlimsCoolMod.Content.Biomes;
using CotlimsCoolMod.Content.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CotlimsCoolMod.Content.Tiles
{
    public class CotGrassBlock : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = true;
            //Main.tileMergeDirt[Type] = true;
            Main.tileBlockLight[Type] = true;
            TileID.Sets.SpreadOverground[Type] = true;
            //Main.tileMoss[Type] = true;
            TileID.Sets.Grass[Type] = true;
            TileID.Sets.ResetsHalfBrickPlacementAttempt[Type] = true;
            TileID.Sets.Conversion.MergesWithDirtInASpecialWay[Type] = true; 
            TileID.Sets.Conversion.Grass[Type] = true;
            TileID.Sets.CanBeDugByShovel[Type] = true;
            TileID.Sets.DoesntPlaceWithTileReplacement[Type] = true;
            TileID.Sets.ChecksForMerge[Type] = true;

            DustType = ModContent.DustType<Sparkle>();

            AddMapEntry(new Color(200, 200, 200));

        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!effectOnly)
            { Framing.GetTileSafely(i, j).TileType = 0; }
        }
        public override bool CanPlace(int i, int j)
        {
            //Main.NewText(Framing.GetTileSafely(i, j).TileType);
            //return Framing.GetTileSafely(i, j).HasTile;
            return true;
        }
        public override void PlaceInWorld(int i, int j, Item item)
        {
            /*8if (Framing.GetTileSafely(i, j).TileType == 0)
            {
                Framing.GetTileSafely(i, j).ResetToType(Type);
            }*/

        }

        public override void ChangeWaterfallStyle(ref int style)
        {
            style = ModContent.GetInstance<ExampleWaterfallStyle>().Slot;
        }
    }
}