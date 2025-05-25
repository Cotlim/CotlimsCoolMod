using CotlimsCoolMod.Common.Config;
using CotlimsCoolMod.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using tModPorter;

namespace CotlimsCoolMod.Common.Config
{
    public class DebugConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(true)]
        public bool DestabilizeTiles = true;
    }


}
public static class Conf
{
    public static void Save()
    {
        try
        {
            ConfigManager.Save(C);
        }
        catch
        {
            Log.Error("An error occurred while manually saving ModConfig!.");
        }
    }

    // Instance of the Config class
    // Use it like 'Conf.C.YourConfigField' for easy access to the config values
    public static DebugConfig C => ModContent.GetInstance<DebugConfig>();
}
