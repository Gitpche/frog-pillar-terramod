using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;

namespace MyMod.Content.NPCs
{
    public class BrimstoneBull : ModNPC
    {
        public override string Texture => "MyMod/Content/NPCs/BrimstoneBull";

        public override void SetStaticDefaults() {
            Main.npcFrameCount[NPC.type] = 1;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire] = true;
            NPCID.Sets.SpecificDebuffImmunity[Type][BuffID.OnFire3] = true;
        }

        public override void SetDefaults() {
            NPC.width = 70;
            NPC.height = 60;
            NPC.damage = 80;      
            NPC.defense = 40;
            NPC.lifeMax = 1200;    
            NPC.HitSound = SoundID.NPCHit41;
            NPC.DeathSound = SoundID.NPCDeath44;
            NPC.value = 1000f;
            NPC.knockBackResist = 0.2f;
            NPC.lavaImmune = true;
            NPC.aiStyle = 26;      
            AIType = NPCID.Unicorn;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo) {
            target.AddBuff(BuffID.OnFire3, 180);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) {
            if (spawnInfo.Player.ZoneUnderworldHeight) {
                return 0.5f;
            }
            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot) {
            npcLoot.Add(ItemDropRule.Common(ItemID.Burger, 5));
            npcLoot.Add(ItemDropRule.Common(ItemID.GoldCoin, 10));
        }

        public override void AI() {
            if (NPC.velocity.X != 0) {
                Dust d = Dust.NewDustDirect(NPC.position, NPC.width, NPC.height, DustID.Torch, 0f, 0f, 100, default, 2f);
                d.noGravity = true;
            }
        }
    }
}