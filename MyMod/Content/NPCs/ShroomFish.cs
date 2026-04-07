using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;

namespace MyMod.Content.NPCs
{
    public class ShroomFish : ModNPC
    {
        // Явно указываем путь, чтобы игра не гадала
        public override string Texture => "MyMod/Content/NPCs/ShroomFish";

        public override void SetStaticDefaults() {
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults() {
            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 10;
            NPC.lifeMax = 100;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.aiStyle = 69; // ИИ Рыброна
            AIType = NPCID.DukeFishron;
        }


        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
            // Спавним в Аду
            return spawnInfo.Player.ZoneUnderworldHeight ? 0.3f : 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot) {
    // Добавляем правило: Шанс 1 из 10 (10%) на выпадение Тунца
            npcLoot.Add(ItemDropRule.Common(ItemID.Tuna, 1)); 
}
    }
}