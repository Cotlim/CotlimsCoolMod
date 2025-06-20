using CotlimsCoolMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CotlimsCoolMod.Content.Items
{
    public class FinalBlaster : ModItem
    {
        public int Quadrant = 0;

        public override void SetDefaults()
        {
            // Modders can use Item.DefaultToRangedWeapon to quickly set many common properties, such as: useTime, useAnimation, useStyle, autoReuse, DamageType, shoot, shootSpeed, useAmmo, and noMelee. These are all shown individually here for teaching purposes.

            // Common Properties
            Item.width = 52; // Hitbox width of the item.
            Item.height = 70; // Hitbox height of the item.
            Item.rare = ItemRarityID.Green; // The color that the item's name will be in-game.

            // Use Properties
            Item.useTime = 100; // The item's use time in ticks (60 ticks == 1 second.)
            Item.useAnimation = Item.useTime; // The length of the item's use animation in ticks (60 ticks == 1 second.)
            Item.useStyle = ItemUseStyleID.Shoot; // How you use the item (swinging, holding out, etc.)
            Item.autoReuse = true; // Whether or not you can hold click to automatically use it again.
                                   //Item.UseSound = SoundID.Item36; // The sound that this item plays when used.

            // Weapon Properties
            Item.DamageType = DamageClass.Magic; // Sets the damage type to ranged.
            Item.damage = 50; // Sets the item's damage. Note that projectiles shot by this weapon will use its and the used ammunition's damage added together.
            Item.knockBack = 6f; // Sets the item's knockback. Note that projectiles shot by this weapon will use its and the used ammunition's knockback added together.
            Item.noMelee = true; // So the item's animation doesn't do damage.
            Item.channel = true;

            // Gun Properties
            Item.shoot = ModContent.ProjectileType<FinalBlasterLoadProjectile>(); // For some reason, all the guns in the vanilla source have this.
            Item.shootSpeed = 10f; // The speed of the projectile (measured in pixels per frame.)
            Item.useAmmo = AmmoID.FallenStar; // The "ammo Id" of the ammo item that this weapon uses. Ammo IDs are magic numbers that usually correspond to the item id of one item that most commonly represent the ammo type.
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.ownedProjectileCounts[type] < 1)
            {
                float pich = ((float)Item.useTime / (float)player.itemTime - 1);

                float speedup = (float)Math.Pow(2, -pich);

                float isItDownBeat = (Quadrant == 2 || Quadrant == 3) ? 1 : 0;

                var p = Projectile.NewProjectileDirect(source, player.Center, velocity,
                        type, damage, knockback, player.whoAmI, ai0: 119, ai1: pich, ai2: isItDownBeat);

                Quadrant++;
                Quadrant %= 4;
            }

            return false;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 10)
                .Register();
        }

        // This method lets you adjust position of the gun in the player's hands. Play with these values until it looks good with your graphics.
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 1f * Item.direction);
        }
    }
}
