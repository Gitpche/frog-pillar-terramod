using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace Crack.NPCs
{
    [AutoloadBossHead]
    public class CrazyFrogPillar : ModNPC
    {
        public override void SetDefaults() {
            NPC.width = 120;
            NPC.height = 200;
            NPC.damage = 0;
            NPC.defense = 20;
            NPC.lifeMax = 10000;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.immortal = true;
            NPC.dontTakeDamage = true;
        }

        public override void AI() {
            // Если убито меньше 50 лягушек (счетчик в ai[1])
            if (NPC.ai[1] < 50) {
                if (Main.rand.NextBool(30)) {
                    Vector2 spawnPos = NPC.Center + new Vector2(Main.rand.Next(-500, 500), Main.rand.Next(-500, 500));
                    NPC.NewNPC(NPC.GetSource_FromAI(), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<MiniFrog>());
                }
                Lighting.AddLight(NPC.Center, Color.Cyan.ToVector3() * 2f);
            } 
            else {
                // Снимаем щит
                NPC.immortal = false;
                NPC.dontTakeDamage = false;
                Lighting.AddLight(NPC.Center, Color.Red.ToVector3() * 3f);
            }
        }
    }
}