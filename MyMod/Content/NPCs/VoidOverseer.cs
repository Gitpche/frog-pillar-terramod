using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using MyMod.Content.Items;

namespace MyMod.Content.NPCs
{
    public class VoidOverseer : ModNPC
    {
        public int KillCount { 
            get => (int)NPC.ai[0];
            set => NPC.ai[0] = value;
        }

        public override void SetDefaults() {
            NPC.width = 100;
            NPC.height = 100;
            NPC.damage = 100;
            NPC.defense = 50;
            NPC.lifeMax = 500000;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = Item.buyPrice(gold: 50);
        }

        public override void AI() {
            NPC.TargetClosest(true);
            Player player = Main.player[NPC.target];

            // ФАЗА 1: Ожидание 74 убийств
            if (KillCount < 74) {
                NPC.velocity = Vector2.Zero;
                NPC.dontTakeDamage = true;
                
                if (Main.netMode != NetmodeID.MultiplayerClient && Main.rand.NextBool(120)) {
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + Main.rand.Next(-400, 400), (int)NPC.Center.Y + Main.rand.Next(-400, 400), ModContent.NPCType<VoidServant>());
                }
            } 
            // ФАЗА 2: Движение и стрельба
            else {
                NPC.dontTakeDamage = false;

                Vector2 targetCenter = player.Center;
                Vector2 direction = targetCenter - NPC.Center;
                float distance = direction.Length();

                // Ещё более медленные настройки скорости
                // 5f — обычный полет, 2f — если он совсем рядом с тобой
                float speed = (distance > 250f) ? 5f : 2f; 

                direction.Normalize();
    
                // Снижаем коэффициент Lerp с 0.03f до 0.015f. 
                // Теперь босс будет оооочень плавно разгоняться.
                NPC.velocity = Vector2.Lerp(NPC.velocity, direction * speed, 0.015f);

                NPC.ai[1]++; 
                int shootInterval = (NPC.life < NPC.lifeMax * 0.2f) ? 300 : 600; 

                if (NPC.ai[1] >= shootInterval) {
                    for (int i = 0; i < 5; i++) {
                        Vector2 vel = new Vector2(0, -1).RotatedBy(MathHelper.ToRadians(-30 + i * 15)) * 10f;
                        int proj = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DestructibleProjectile>());
                        
                        // Снаряды становятся прочнее в фазе ярости
                        if (NPC.life < NPC.lifeMax * 0.2f) {
                            Main.npc[proj].lifeMax = 3000;
                            Main.npc[proj].life = 3000;
                        }
                    }
                    NPC.ai[1] = 0;
                }
            }
        } // Конец метода AI

        public override void BossLoot(ref string name, ref int potionType) {
            // Шанс выпадения фрагмента пустоты
            Item.NewItem(NPC.GetSource_Loot(), NPC.getRect(), ModContent.ItemType<VoidFragment>());
        }
    }
}