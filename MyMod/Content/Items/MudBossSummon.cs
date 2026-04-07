using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace MyMod.Content.Items
{
    public class MudBossSummon : ModItem
    {
        public override void SetStaticDefaults() {
            // Описание предмета
            // "Призывает Грязевое Око. Не забудь зажать мышь!"
        }

        public override void SetDefaults() {
            Item.width = 20;
            Item.height = 20;
            Item.maxStack = 20;
            Item.value = Item.sellPrice(0, 0, 10, 0);
            Item.rare = ItemRarityID.Blue;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.useStyle = ItemUseStyleID.HoldUp; // Игрок поднимает предмет вверх
            Item.consumable = true; // Тратится при использовании
        }

        public override bool CanUseItem(Player player) {
            // Можно использовать только если босс еще не жив
            return !NPC.AnyNPCs(ModContent.NPCType<NPCs.MudBoss>());
        }

        public override bool? UseItem(Player player) {
            if (player.whoAmI == Main.myPlayer) {
                // Играем звук вызова босса
                SoundEngine.PlaySound(SoundID.Roar, player.position);

                int type = ModContent.NPCType<NPCs.MudBoss>();

                if (Main.netMode != NetmodeID.MultiplayerClient) {
                    // Если одиночная игра, просто спавним
                    NPC.SpawnOnPlayer(player.whoAmI, type);
                }
                else {
                    // Если мультиплеер, отправляем пакет серверу
                    NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, number: player.whoAmI, number2: type);
                }
            }
            return true;
        }

        public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 10) // 10 земли
                .AddIngredient(ItemID.Gel, 5)        // 5 геля
                .AddTile(TileID.WorkBenches)         // Крафтим на верстаке
                .Register();
        }
    }
}