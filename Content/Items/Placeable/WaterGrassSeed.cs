﻿
using CotlimsCoolMod.Content.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace CotlimsCoolMod.Content.Items.Placeable
{
    public class WaterGrassSeed : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 100;
        }

        public override void SetDefaults()
        {

            Item.DefaultToPlaceableTile(ModContent.TileType<Tiles.WaterGrassTile>());
            Item.width = 22;
            Item.height = 18;
            Item.channel = true;
            //ItemID.Sets.ExtractinatorMode[Item.type] = Item.type;
            ItemID.Sets.GrassSeeds[Type] = true;
            CotlimsCoolMod.GrassTileRelationship.Add(
                new(Type, TileID.Sand, ModContent.TileType<Tiles.WaterGrassTile>())
                );
            Item.createTile = -1;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            CreateRecipe(10)
                .AddIngredient(ItemID.DirtBlock)
                .Register();
        }

        public override bool? UseItem(Player player)
        {
            int i = Player.tileTargetX;
            int j = Player.tileTargetY;

            Tile tile = Framing.GetTileSafely(Player.tileTargetX, Player.tileTargetY);

            if (tile.HasTile && !tile.IsActuated && player.channel)
            {
                foreach (var l in CotlimsCoolMod.GrassTileRelationship.Where(t => t.Item1 == Type))
                {
                    if (tile.TileType == l.Item2)
                    {
                        Main.tile[i, j].TileType = (ushort)l.Item3;

                        SoundEngine.PlaySound(SoundID.Dig, player.Center);
                        WorldGen.TileFrame(i, j);
                        WorldGen.DiamondTileFrame(i, j);
                        NetMessage.SendTileSquare(-1, i, j, 1);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
