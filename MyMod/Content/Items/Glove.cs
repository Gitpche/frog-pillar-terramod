using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyMod.Content.Items
{
    public class Glove : ModItem
    {
        public override void SetStaticDefaults() { }

        public override void SetDefaults() {
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.NPCDeath1; // Звук хлюпанья искажения
            Item.autoReuse = true;
        }

        public override bool? UseItem(Player player) {
            if (Main.myPlayer == player.whoAmI) {
                // Спавним Eater of Souls (ID 6)
                int index = NPC.NewNPC(player.GetSource_ItemUse(Item), (int)player.Center.X, (int)player.Center.Y, NPCID.EaterofSouls);
                
                if (index >= 0 && index < 200) {
                    NPC eater = Main.npc[index];
                    eater.friendly = true; 
                    eater.noTileCollide = true;
                    
                    // Стандартные статы Пожирателя:
                    eater.lifeMax = 40; 
                    eater.life = 40;
                    eater.damage = 22; 
                    eater.defense = 8;
                    
                    eater.ai[3] = 888; // Новая метка для Пожирателя
                    
                    // Делаем его чуть-чуть фиолетовым/светящимся, чтобы отличить от диких
                    eater.color = new Color(200, 100, 255); 
                }
            }
            return true;
        }
    }

    public class EaterLogic : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override void AI(NPC npc) {
            // Проверяем, наш ли это Пожиратель
            if (npc.type == NPCID.EaterofSouls && npc.ai[3] == 888) {
                float maxDist = 900f;
                NPC target = null;

                // Ищем врага
                for (int i = 0; i < Main.maxNPCs; i++) {
                    NPC en = Main.npc[i];
                    if (en.active && !en.friendly && en.lifeMax > 5 && en.whoAmI != npc.whoAmI) {
                        float dist = Vector2.Distance(npc.Center, en.Center);
                        if (dist < maxDist) {
                            maxDist = dist;
                            target = en;
                        }
                    }
                }

                if (target != null) {
                    // Летим к врагу быстрее мыши!
                    Vector2 move = target.Center - npc.Center;
                    move.Normalize();
                    npc.velocity = Vector2.Lerp(npc.velocity, move * 10f, 0.1f);

                    // Урон при касании (механика 1.4.4)
                    if (npc.Hitbox.Intersects(target.Hitbox)) {
                        var hit = target.CalculateHitInfo(npc.damage, 1, false, 2f);
                        target.StrikeNPC(hit);
                        
                        // Отскок, чтобы не застревал
                        npc.velocity = -move * 5f;
                    }
                }
                else {
                    // Возвращаемся к игроку, если скучно
                    Player p = Main.player[npc.target];
                    if (Vector2.Distance(npc.Center, p.Center) > 150f) {
                        Vector2 toPlayer = p.Center - npc.Center;
                        toPlayer.Normalize();
                        npc.velocity = Vector2.Lerp(npc.velocity, toPlayer * 7f, 0.03f);
                    }
                }
            }
        }
    }
}