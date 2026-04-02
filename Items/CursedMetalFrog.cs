using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Crack.NPCs;

namespace Crack.Items
{
    public class CursedMetalFrog : ModItem
    {
        public override void SetDefaults() {
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.consumable = true;
            Item.rare = ItemRarityID.Purple;
        }

        public override bool? UseItem(Player player) {
            if (Main.netMode != NetmodeID.MultiplayerClient) {
                // Спавним столб над головой игрока
                NPC.NewNPC(player.GetSource_ItemUse(Item), (int)player.Center.X, (int)player.Center.Y - 400, ModContent.NPCType<CrazyFrogPillar>());
            }
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Roar, player.position);
            return true;
        }

        public override void AddRecipes() {
            CreateRecipe().AddIngredient(ItemID.DirtBlock, 5).AddTile(TileID.WorkBenches).Register();
        }
    }
}