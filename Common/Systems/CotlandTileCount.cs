using CotlimsCoolMod.Content.Biomes;
using CotlimsCoolMod.Content.Tiles;
using CotlimsCoolMod.Helpers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace CotlimsCoolMod.Common.Systems
{
	public class CotlandTileCount : ModSystem
	{
        private static int cotBlockCount = 0;
		private static float cotBlockInfluence = 0f;

		public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts) {
			cotBlockCount = tileCounts[ModContent.TileType<CotGrassBlock>()];
        }

        public static float CotBlockInfluence => cotBlockInfluence;

        public static void UpdateInfluence()
        {
            if (!Main.gameMenu)
            {
                cotBlockInfluence = MathHelper.Lerp(cotBlockInfluence, Math.Clamp(cotBlockCount, 0f, 40f) / 40f, 0.05f);
            }
            else
            {
                cotBlockInfluence = MathHelper.Lerp(cotBlockInfluence, 0f, 0.05f);
            }
        }
    }
}
