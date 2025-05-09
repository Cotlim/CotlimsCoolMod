using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CotlimsCoolMod.Content.Items.Placeable
{
	public class CotCampfire : ModItem
	{
		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.ExampleCampfire>(), 0);
		}

		public override void AddRecipes() {
			CreateRecipe()
				.AddRecipeGroup(RecipeGroupID.Wood, 10)
				.AddIngredient<CotTorch>(5)
				.Register();
		}
	}
}
