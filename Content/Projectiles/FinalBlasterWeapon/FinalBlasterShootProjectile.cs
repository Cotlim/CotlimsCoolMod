using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace CotlimsCoolMod.Content.Projectiles
{
    public class FinalBlasterShootProjectile : ModProjectile
    {
        public override string Texture => "CotlimsCoolMod/Content/Projectiles/FinalBlasterWeapon/FinalBlasterShootProjectile";

        private Vector2 _lastPos;

        private bool _firstTime = true;

        private Player _owner;

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
            Projectile.penetrate = -1;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.usesLocalNPCImmunity = true;


        }

        public override void AI()
        {
            if (_firstTime)
            {
                _firstTime = false;

                Projectile.rotation = Projectile.velocity.ToRotation();
                _owner = Main.player[Projectile.owner];
                _lastPos = Projectile.Center;
            }

            //Decrese velocity
            Projectile.velocity *= (float)Math.Pow(Timer / 180f, 0.1f);

            //If projectile in blocks, decrese velocity even more
            if (Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
            {
                Projectile.velocity = Vector2.Normalize(Projectile.velocity) * Math.Min(Projectile.velocity.Length(), 1f);
            }

            //Settig damage depending on velcity
            Projectile.damage = (int)(_owner.GetWeaponDamage(_owner.HeldItem) * Projectile.velocity.Length() / 5);

            //When projectile far enough from last trail, spawn another
            if ((_lastPos - Projectile.Center).Length() > 23)
            {
                SpawnTrail();
            }

            //Decrement timer
            Timer--;

            //Change opacity depending on time
            if (Timer <= 30 && Timer > 0)
            {
                Projectile.Opacity = 1 - 30 / Timer;
            }
            else if (Timer <= 0)
            {
                Projectile.Kill();
            }

        }

        private void SpawnTrail()
        {
            if (Projectile.owner == Main.myPlayer)
            {
                var newPos = Vector2.Normalize(Projectile.Center - _lastPos) * 23 + _lastPos;
                var p = Projectile.NewProjectile(Projectile.GetSource_FromThis(), newPos,
                            Projectile.velocity, ModContent.ProjectileType<FinalBlasterTrailProjectile>(),
                            (int)(Projectile.damage / 1.2f), 1, Projectile.owner, ai0: Timer, ai1: Pitch);
                _lastPos = newPos;
            }
        }
    }
}