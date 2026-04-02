using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace UniverseCracker.Items // ТЕПЕРЬ АДРЕС СОВПАДАЕТ С НАЗВАНИЕМ ПРОЕКТА
{
    public class UniverseCracker : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 5000;
            Item.DamageType = DamageClass.Melee;
            
            // Убедись, что эти цифры близки к размеру твоей картинки!
            Item.width = 96; 
            Item.height = 96;

            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Swing; 
            
            Item.knockBack = 15f;
            Item.value = Item.buyPrice(platinum: 5);
            Item.rare = ItemRarityID.Purple;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(2)) 
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.PurpleCrystalShard);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 0.5f;
            }
            Lighting.AddLight(player.itemLocation, 0.8f, 0.2f, 0.8f);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.DirtBlock, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}