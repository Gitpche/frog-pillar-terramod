using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;

namespace MyMod.Content.NPCs
{
    public class MyGlobalNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) 
        {
            // Проверяем, является ли NPC городским жителем (Town NPC)
            // Это гарантирует, что мы работаем только с НИПами
            if (npc.townNPC) 
            {
                // --- 1. РЫБАК (ANGLER) ---
                if (npc.type == NPCID.Angler) 
                {
                    // 100% шанс на Золотую удочку
                    npcLoot.Add(ItemDropRule.Common(ItemID.GoldenFishingRod, 1));
                    npcLoot.Add(ItemDropRule.Common(ItemID.SonarPotion, 1, 4, 4));
                    npcLoot.Add(ItemDropRule.Common(ItemID.MasterBait, 1, 4, 20));
                }

                // --- 2. МЕДСЕСТРА (NURSE) ---
                if (npc.type == NPCID.Nurse) 
                {
                    // 5-10 зелий восстановления (100 HP)
                    npcLoot.Add(ItemDropRule.Common(ItemID.RestorationPotion, 1, 5, 10));
                }

                // --- 3. ТОРГОВЕЦ (MERCHANT) ---
                if (npc.type == NPCID.Merchant) 
                {
                    // Дропает свой Сачок (Bug Net), чтобы ловить червей из твоих коробочек бесплатно
                    npcLoot.Add(ItemDropRule.Common(ItemID.BugNet, 1));
                }
            }
        }
    }
}