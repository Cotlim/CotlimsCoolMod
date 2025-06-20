﻿using CotlimsCoolMod.Content.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CotlimsCoolMod.Content.Items.Placeable
{
	public class SandSnowCompound : ModItem
	{
		public override void SetStaticDefaults() {
			Item.ResearchUnlockCount = 100;

			// Set the SandgunAmmoProjectileData to your sandgun projectile with a bonus damage of 10
			ItemID.Sets.SandgunAmmoProjectileData[Type] = new(ModContent.ProjectileType<Projectiles.SandSnowCompoundBallGunProjectile>(), 10);
		}

		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(ModContent.TileType<SandSnowCompoundTile>());
			Item.width = 12;
			Item.height = 12;
			Item.ammo = AmmoID.Sand;
			// Item.shoot and Item.damage are not used for sand ammo by convention. They would result in undesireable item tooltips.
			// ItemID.Sets.SandgunAmmoProjectileData is used instead.
			Item.notAmmo = true;
		}

		public override void AddRecipes() {
			CreateRecipe(1)
				.AddIngredient(ItemID.SandBlock)
                .AddIngredient(ItemID.SnowBlock)
                .AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}