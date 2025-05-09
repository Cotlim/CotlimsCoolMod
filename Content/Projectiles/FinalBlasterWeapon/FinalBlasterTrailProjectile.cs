using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CotlimsCoolMod.Content.Projectiles
{
    // This example is similar to the Wooden Arrow projectile


    public class FinalBlasterTrailProjectile : ModProjectile
    {

        public override string Texture => "CotlimsCoolMod/Content/Projectiles/FinalBlasterWeapon/FinalBlasterTrailProjectile";
        private float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        private float Pitch
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }

        public override void SetDefaults()
        {
            Projectile.extraUpdates = 3;
            Projectile.height = 38;
            Projectile.width = 38;
            Projectile.hide = true;
            Projectile.tileCollide = false;
            //Projectile.usesIDStaticNPCImmunity = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            ProjectileID.Sets.DontAttachHideToAlpha[Type] = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            base.OnSpawn(source);
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            base.DrawBehind(index, behindNPCsAndTiles, behindNPCs, behindProjectiles, overPlayers, overWiresUI);
            behindProjectiles.Add(index);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = TextureAssets.Projectile[Type].Value;
            Vector2 position = Projectile.Center - Main.screenPosition;
            Rectangle sourceRectangle = texture.Frame(1, 1);
            Vector2 origin = sourceRectangle.Size() / 2f;

            return true;
        }

        public override void AI()
        {
            Projectile.position -= Projectile.velocity;
            Projectile.rotation = Projectile.velocity.ToRotation();

            if (Timer <= 100 && Timer > 0)
            {
                Projectile.Opacity = 1 - 30 / Timer;
            }
            else if (Timer <= 0)
            {
                Projectile.Kill();
            }
            Timer--;


        }

        public override void OnKill(int timeLeft)
        {

        }
    }
}