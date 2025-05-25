using CotlimsCoolMod.Content.Biomes;
using CotlimsCoolMod.Content.Items.Placeable;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CotlimsCoolMod
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class CotlimsCoolMod : Mod
    {
        /// <summary>
        /// Describes what Grass Seeds (0) can be put on Tile (1) and resulting Tile (2)
        /// </summary>
        public static List<Tuple<int, int, int>> GrassTileRelationship = new();
        public override void Load()
        {
            On_Main.SetBackColor += DaylandSurfaceBiome.SetCotlandCollor;
            Terraria.GameContent.On_SmartCursorHelper.Step_GrassSeeds += SunGrassSeed.SmartCursorCustomSeedModification;
        }

        public override void Unload()
        {
            On_Main.SetBackColor -= DaylandSurfaceBiome.SetCotlandCollor;
            Terraria.GameContent.On_SmartCursorHelper.Step_GrassSeeds -= SunGrassSeed.SmartCursorCustomSeedModification;
        }

        public static bool IsTileSolid(int x, int y)
        {
            Tile tile = Framing.GetTileSafely(x, y);
            return tile.HasTile && tile.HasUnactuatedTile && Main.tileSolid[tile.TileType];
        }
    }
}
