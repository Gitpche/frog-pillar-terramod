using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Crack.NPCs
{
    public class MiniFrog : ModNPC
    {
        public override void SetStaticDefaults() {
            Main.npcFrameCount[NPC.type] = 1; 
        }

        public override void SetDefaults() {
            NPC.width = 30;
            NPC.height = 30;
            NPC.damage = 45;
            NPC.defense = 10;
            NPC.lifeMax = 300; 
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.aiStyle = 69; // Ванильный AI прыгающей лягушки/слизня
        }

        public override void OnKill() {
            // Логика: при смерти прибавляет счетчик столбу
            for (int i = 0; i < Main.maxNPCs; i++) {
                NPC other = Main.npc[i];
                if (other.active && other.type == ModContent.NPCType<CrazyFrogPillar>()) {
                    other.ai[1]++; 
                    if (Main.netMode != NetmodeID.Server) {
                        CombatText.NewText(other.getRect(), Color.Cyan, (int)other.ai[1] + "/50", true);
                    }
                    break;
                }
            }
        }
    }
}