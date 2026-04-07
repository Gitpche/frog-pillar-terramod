using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MyMod.Content.NPCs
{
    public class GlobalNPCShop : GlobalNPC
    {
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            // --- 1. ТОРГОВЕЦ (Merchant) ---
            if (npc.type == NPCID.Merchant)
            {
                Item wings = new Item();
                wings.SetDefaults(ItemID.CreativeWings);
                wings.shopCustomPrice = 100000; 

                Item conch = new Item();
                conch.SetDefaults(ItemID.MagicConch);
                conch.shopCustomPrice = 10000;

                AddToShop(items, wings);
                AddToShop(items, conch);
            }

            // --- 2. ОРУЖЕЙНИК (Arms Dealer) ---
            if (npc.type == NPCID.ArmsDealer)
            {
                if (NPC.downedBoss3)
                {
                    Item phoenix = new Item();
                    phoenix.SetDefaults(ItemID.PhoenixBlaster);
                    phoenix.shopCustomPrice = 150000; 
                    AddToShop(items, phoenix);
                }
            }

            // --- 3. ВОЛШЕБНИК (Wizard) ---
            if (npc.type == NPCID.Wizard)
            {
                Item frostStaff = new Item();
                frostStaff.SetDefaults(ItemID.FrostStaff);
                frostStaff.shopCustomPrice = 200000;

                AddToShop(items, frostStaff);
            }

            // --- 4. ЗНАХАРЬ (Witch Doctor) ---
            if (npc.type == NPCID.WitchDoctor)
            {
                Item necklace = new Item();
                necklace.SetDefaults(ItemID.PygmyNecklace);
                necklace.shopCustomPrice = 200000; 

                AddToShop(items, necklace);
            }

            // --- 5. РЫБАК (Angler) ---
            // Убрали лишнее объявление метода, оставили только логику
            if (npc.type == NPCID.Angler)
            {
                // Золотая удочка
                Item goldRod = new Item(ItemID.GoldenFishingRod);
                goldRod.shopCustomPrice = 50000;
                AddToShop(items, goldRod);

                // Наживка
                Item masterBait = new Item(ItemID.MasterBait);
                masterBait.shopCustomPrice = 2000;
                AddToShop(items, masterBait);

                // Зелье сонара
                Item sonarPotion = new Item(ItemID.SonarPotion);
                sonarPotion.shopCustomPrice = 1000;
                AddToShop(items, sonarPotion);
                
                // Квестовая рыба дня (КРЭК!)
                int questFishType = Main.anglerQuestItemNetIDs[Main.anglerQuest];
                Item questFish = new Item(questFishType);
                questFish.shopCustomPrice = 5000;
                AddToShop(items, questFish);
            }
        } // Конец метода ModifyActiveShop

        private void AddToShop(Item[] items, Item newItem)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null || items[i].type == ItemID.None)
                {
                    items[i] = newItem;
                    break;
                }
            }
        }
    } 
}