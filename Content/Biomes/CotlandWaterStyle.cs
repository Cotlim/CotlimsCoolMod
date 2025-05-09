using CotlimsCoolMod.Content.Dusts;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CotlimsCoolMod.Content.Biomes
{
    public class CotlandWaterStyle : ModWaterStyle
    {


        private Asset<Texture2D> rainTexture;
        public override void Load()
        {
            rainTexture = Mod.Assets.Request<Texture2D>("Content/Biomes/ExampleRain");
        }

        public override int ChooseWaterfallStyle()
        {
            return ModContent.GetInstance<ExampleWaterfallStyle>().Slot;
        }

        public override int GetSplashDust()
        {
            return ModContent.DustType<CotlandSolution>();
        }

        public override int GetDropletGore()
        {
            return ModContent.GoreType<ExampleDroplet>();
        }

        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = Main.DiscoColor.R / 255f;
            g = Main.DiscoColor.G / 255f;
            b = Main.DiscoColor.B / 255f;
        }

        public override Color BiomeHairColor()
        {
            return Color.White;
        }

        public override byte GetRainVariant()
        {
            return (byte)Main.rand.Next(3);
        }

        public override Asset<Texture2D> GetRainTexture() => rainTexture;
    }
}