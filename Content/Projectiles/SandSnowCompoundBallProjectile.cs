using CotlimsCoolMod.Content.Items.Placeable;
using CotlimsCoolMod.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

// This file contains ExampleSandBallProjectile, ExampleSandBallFallingProjectile, and ExampleSandBallGunProjectile.
// ExampleSandBallFallingProjectile and ExampleSandBallGunProjectile inherit from ExampleSandBallProjectile, allowing cleaner code and shared logic.
// ExampleSandBallFallingProjectile is the projectile that spawns when the ExampleSand tile falls.
// ExampleSandBallGunProjectile is the projectile that is shot by the Sandgun weapon.
// Both projectiles share the same aiStyle, ProjAIStyleID.FallingTile, but the AIType line in ExampleSandBallGunProjectile ensures that specific logic of the aiStyle is used for the sandgun projectile.
// It is possible to make a falling projectile not using ProjAIStyleID.FallingTile, but it is a lot of code.
namespace CotlimsCoolMod.Content.Projectiles
{
	public abstract class SandSnowCompoundBallProjectile : ModProjectile
	{
        public override string Texture => "CotlimsCoolMod/Content/Projectiles/SandSnowCompoundBallProjectile";
        public override void SetStaticDefaults() {
			ProjectileID.Sets.FallingBlockDoesNotFallThroughPlatforms[Type] = true;
			ProjectileID.Sets.ForcePlateDetection[Type] = true;
		}
	}

	public class SandSnowCompoundBallFallingProjectile : SandSnowCompoundBallProjectile
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			ProjectileID.Sets.FallingBlockTileItem[Type] = new(ModContent.TileType<SandSnowCompoundTile>(), ModContent.ItemType<SandSnowCompound>());
		}

		public override void SetDefaults() {
			// The falling projectile when compared to the sandgun projectile is hostile.
			Projectile.CloneDefaults(ProjectileID.EbonsandBallFalling);
		}

        public override void PostAI()
        {
            base.PostAI();
            Projectile.velocity *= 0.8f;
        }
    }

	public class SandSnowCompoundBallGunProjectile : SandSnowCompoundBallProjectile
	{
		public override void SetStaticDefaults() {
			base.SetStaticDefaults();
			ProjectileID.Sets.FallingBlockTileItem[Type] = new(ModContent.TileType<SandSnowCompoundTile>());
		} 

		public override void SetDefaults() {
			// The sandgun projectile when compared to the falling projectile has a ranged damage type, isn't hostile, and has extraupdates = 1.
			// Note that EbonsandBallGun has infinite penetration, unlike SandBallGun
			Projectile.CloneDefaults(ProjectileID.EbonsandBallGun);
            AIType = ProjectileID.EbonsandBallGun; // This is needed for some logic in the ProjAIStyleID.FallingTile code.
		}

        public override void PostAI()
        {
            base.PostAI();
			ItemID.SandBlock;
			ItemID.SnowBlock
        }
    }
}