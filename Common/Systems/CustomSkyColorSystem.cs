using CotlimsCoolMod.Content.Biomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace CotlimsCoolMod.Common.Systems
{
    internal class CustomSkyColorSystem : ModSystem
    {
        public override void OnWorldLoad()
        {
            On_Main.SetBackColor += DaylandSurfaceBiome.SetCotlandCollor;
        }
        public override void OnWorldUnload()
        {
            On_Main.SetBackColor -= DaylandSurfaceBiome.SetCotlandCollor;
        }

    }
}
