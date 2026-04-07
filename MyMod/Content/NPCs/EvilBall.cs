using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.ItemDropRules;

namespace MyMod.Content.NPCs 
{
    public class EvilBall : ModNPC
    {
        public override void SetStaticDefaults() {
            // Тот самый 1 кадр безумия
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults() {
            NPC.width = 40; 
            NPC.height = 40;
            NPC.damage = 70; // Твой фирменный урон
            NPC.defense = 18;
            NPC.lifeMax = 350;
            NPC.HitSound = SoundID.NPCHit1; 
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 150f;
            NPC.knockBackResist = 0.7f; // Чтобы BasicSword его "футболил"
            
            // Поведение шипастого ледяного слизня (стреляет и прыгает)
            NPC.aiStyle = 1; 
            AIType = NPCID.SpikedIceSlime; 
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
            // Если игрок находится в снежном биоме (ZoneSnow)
            if (spawnInfo.Player.ZoneSnow|| spawnInfo.Player.ZoneUnderworldHeight) {
                // Прямой шанс 0.5f (как у твоего быка в аду)
                // Это сделает мячик ОЧЕНЬ частым гостем в снегах
                return 0.5f; 
    }

            // В остальных местах не спавним
            return 0f;
}

        public override void ModifyNPCLoot(NPCLoot npcLoot) {
            // 1. Обычный дроп: Снежки (5-15 шт, шанс 100%)
            npcLoot.Add(ItemDropRule.Common(ItemID.Snowball, 1, 5, 15));

            // 2. Крэк-дроп: 500 блоков снега (Шанс 1 к 100)
            npcLoot.Add(ItemDropRule.Common(ItemID.SnowBlock, 100, 500, 500));

            // 3. Золотой билет: Slime Staff (Шанс 1 к 10,000)
            // Безопасно, редко, легендарно!
            npcLoot.Add(ItemDropRule.Common(ItemID.SlimeStaff, 10000)); 
        }
    }
}