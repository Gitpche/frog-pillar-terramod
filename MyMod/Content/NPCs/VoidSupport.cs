using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyMod.Content.NPCs
{
    // Слуга (74 штуки)
    public class VoidServant : ModNPC
    {
        public override void SetDefaults() {
            NPC.width = 40;
            NPC.height = 40;
            NPC.damage = 60;
            NPC.lifeMax = 5000;
            NPC.noGravity = true;
            NPC.aiStyle = 22;
        }

        public override void OnKill() {
            int bossType = ModContent.NPCType<VoidOverseer>();
            for (int i = 0; i < Main.maxNPCs; i++) {
                if (Main.npc[i].active && Main.npc[i].type == bossType) {
                    Main.npc[i].ai[0]++;
                    Main.NewText($"Крэкнуто слуг: {Main.npc[i].ai[0]} / 74", 175, 75, 255);
                    break;
                }
            }
        }
    }

    // Снаряд, который можно сбить
    public class DestructibleProjectile : ModNPC
    {
        public override void SetDefaults() {
            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 80;
            NPC.lifeMax = 2000;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
        }

        public override void AI() {
            NPC.rotation += 0.2f;
            if (Main.rand.NextBool(3)) {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleCrystalShard);
            }
        }
    }
}