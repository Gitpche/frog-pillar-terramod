using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace MyMod.Content.Items
{
    // 1. СНАЧАЛА ОБЪЯВЛЯЕМ КЛАСС ДЛЯ ТРЯСКИ
    public class VoidScreenShake : ModPlayer 
    {
        public int ScreenShakeTimer;
        public override void ModifyScreenPosition() 
        {
            if (ScreenShakeTimer > 0) 
            {
                Main.screenPosition += Main.rand.NextVector2Circular(15f, 15f);
                ScreenShakeTimer--;
            }
        }
    }

    // 2. ТЕПЕРЬ САМА ПУШКА
    public class VoidSingularity : ModItem
    {
        public override void SetDefaults() 
        {
            Item.damage = 1000; 
            Item.DamageType = DamageClass.Magic;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 60; 
            Item.useAnimation = 60;
            Item.useStyle = ItemUseStyleID.HoldUp; 
            Item.noMelee = true; 
            Item.knockBack = 15f;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item122; 
            Item.mana = 30; 
            Item.autoReuse = false;
        }

        public override bool? UseItem(Player player) 
        {
            Vector2 targetPos = Main.MouseWorld;

            // БАБАХ (Эффект Shadowflame)
            for (int i = 0; i < 150; i++) 
            {
                Vector2 speed = Main.rand.NextVector2Circular(20f, 20f);
                Dust d = Dust.NewDustPerfect(targetPos, DustID.Shadowflame, speed, 0, default, 4f);
                d.noGravity = true;
            }

            // АННИГИЛЯЦИЯ
            float radius = 50 * 16f; 
            for (int k = 0; k < Main.maxNPCs; k++) 
            {
                NPC npc = Main.npc[k];
                if (npc.active && !npc.friendly && Vector2.Distance(targetPos, npc.Center) < radius) 
                {
                    NPC.HitInfo hit = new NPC.HitInfo();
                    hit.Damage = 1000;
                    hit.Knockback = 15f;
                    hit.HitDirection = (npc.Center.X > targetPos.X) ? 1 : -1;
                    npc.StrikeNPC(hit);
                }
            }

            // ТЕПЕРЬ ЭТО ТОЧНО СРАБОТАЕТ
            if (Main.netMode != NetmodeID.Server) 
            {
                player.GetModPlayer<VoidScreenShake>().ScreenShakeTimer = 30;
            }

            return true;
        }

        public override void AddRecipes() 
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 10)      // 10 земли (основа материи)
                .AddIngredient(ItemID.Granite, 10)        // 10 гранита (структура)
                .AddIngredient(ItemID.FallenStar, 10)     // 10 звезд (энергия космоса)
                .AddIngredient(ItemID.FragmentNebula, 40) // 40 фрагментов Небулы (магия чистого разрушения)
                .AddTile(TileID.LunarCraftingStation)     // Создаем только на Манипуляторе Древних!
                .Register();
        }
    }
}