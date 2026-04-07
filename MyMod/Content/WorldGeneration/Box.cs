using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.GameContent.Generation;
using Terraria.IO;

namespace MyMod.Content.WorldGeneration
{
    public class Box : ModSystem
    {
        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
        {
            int index = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
            if (index != -1)
            {
                tasks.Insert(index + 1, new PassLegacy("Single Obsidian Vault", (progress, config) => {
                    progress.Message = "Searching for a perfect granite spot...";
                    bool boxBuilt = false;

                    // Сканируем мир от поверхности вниз
                    for (int x = 100; x < Main.maxTilesX - 100 && !boxBuilt; x++)
                    {
                        for (int y = (int)Main.worldSurface; y < Main.maxTilesY - 100; y++)
                        {
                            Tile tile = Main.tile[x, y];
                            if (tile.HasTile && tile.TileType == TileID.Granite)
                            {
                                BuildFinalBox(x, y);
                                boxBuilt = true; 
                                break; 
                            }
                        }
                    }
                }));
            }
        }

        private void BuildFinalBox(int x, int y)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    // 1. Сначала СНОСИМ старые стены, потом ставим Деревянные
                    WorldGen.KillWall(x + i, y + j);
                    WorldGen.PlaceWall(x + i, y + j, WallID.Wood);

                    if (i == 0 || i == 9 || j == 0 || j == 9)
                    {
                        // 2. Обсидиановая рамка
                        WorldGen.PlaceTile(x + i, y + j, TileID.Obsidian, true, true);
                    }
                    else
                    {
                        // 3. Пустота внутри
                        WorldGen.KillTile(x + i, y + j);
                    }
                }
            }

            // 4. ФАКЕЛ (ставим на деревянную стену, которую только что создали)
            WorldGen.PlaceTile(x + 5, y + 5, TileID.Torches, true, false);

            // 5. СУНДУК (на пол из обсидиана)
            WorldGen.PlaceChest(x + 4, y + 8, TileID.Containers, false, 0);
            
            // БИИИИИИИЯУУУУУ! Одна коробка на весь мир создана.
        }
    }
}