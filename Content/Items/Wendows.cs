using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CotlimsCoolMod.Content.Items
{
	public class Wendows : ModItem
	{
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.DirtBlock, 10)
				.Register();
		}
	}
}
