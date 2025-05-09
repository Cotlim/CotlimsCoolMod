using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CotlimsCoolMod.Content.Projectiles
{
    // This example is similar to the Wooden Arrow projectile
    public class FasterBBoulderLineProjectile : ModProjectile
	{
        public override string Texture => "Terraria/Images/Projectile_0";

		public override void AI() {
            Player owner = Main.player[Projectile.owner];
            Projectile.ai[0]--;
			Projectile.position = owner.position;

            if (Projectile.ai[0] <= 0)
			{
				Projectile.ai[0] = 0;
                for (int i = 0; i < 8; i++)
                {
                    Vector2 newVelocity = Projectile.ai[1].ToRotationVector2() * Projectile.ai[2] / (i + 1);

                    Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.position,
                        newVelocity, ModContent.ProjectileType<FasterBBoulderProjectile>(),
                        Projectile.damage, Projectile.knockBack, Projectile.owner);
                }
                Projectile.Kill();

            }
            

        }

		public override void OnKill(int timeLeft) {
			SoundEngine.PlaySound(SoundID.Dig, Projectile.position); // Plays the basic sound most projectiles make when hitting blocks.
			for (int i = 0; i < 5; i++) // Creates a splash of dust around the position the projectile dies.
			{
				Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.Silver);
				dust.noGravity = true;
				dust.velocity *= 1.5f;
				dust.scale *= 0.9f;
			} 
		}

        
    }
}