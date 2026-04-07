using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;

namespace MyMod.Content.NPCs
{
 // Добавляет иконку босса на карту
    public class MudBoss : ModNPC
    {
        public override void SetStaticDefaults() {
            Main.npcFrameCount[NPC.type] = 1; // Кадры для анимации (глаз моргает/вращается)
        }

        public override void SetDefaults() {
            NPC.width = 100;
            NPC.height = 110;
            NPC.damage = 30;
            NPC.defense = 10;
            NPC.lifeMax = 3000; // Для сида getfixedboy это будет честная битва
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.knockBackResist = 0f; // БОСС НЕ БОИТСЯ ТВОЕГО МЕЧА!
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.npcSlots = 10f;
            NPC.boss = true; // Чтобы появилась полоска ХП и музыка
            
            NPC.aiStyle = 4; // ИИ Глаза Ктулху
            AIType = NPCID.EyeofCthulhu;
        }

        public override void AI() {
            // Если ХП меньше половины, он начинает светиться или менять поведение
            if (NPC.life < NPC.lifeMax / 2) {
                NPC.damage = 50; // Во второй фазе бьет больнее
                // Можно добавить частицы, чтобы он выглядел злым
                if (Main.rand.NextBool(3)) {
                    Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Dirt);
                }
            }
        }

        // Чтобы он не спавнился сам, а только по призыву (или сделай шанс спавна 0.01)
        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
            return 0f; 
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot) {
            // 1. ЖАРЕНАЯ РЫБА (Cooked Fish)
            // Шанс 1/1 (100%), количество от 5 до 10 штук
            npcLoot.Add(ItemDropRule.Common(ItemID.CookedFish, 1, 5, 10));

            // 2. ЗЕЛЬЕ СОНАРА (Sonar Potion)
            // Шанс 1/10 (10%), количество 1 штука
            npcLoot.Add(ItemDropRule.Common(ItemID.SonarPotion, 10));

            // 3. ЗЕМЛЯ (Dirt Block) — Тот самый рофло-дроп
            // Шанс 1/20 (5%), количество ровно 200 штук
            npcLoot.Add(ItemDropRule.Common(ItemID.DirtBlock, 20, 200, 200));
        }
    }
}