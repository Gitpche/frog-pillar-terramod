using Terraria;
using Terraria.ModLoader;

namespace MyMod.Content.Items
{
    public class VoidFragment : ModItem
    {
        public override void SetDefaults() {
            Item.width = 20;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = -12; // Радужная редкость
            Item.value = Item.sellPrice(gold: 20);
        }

        public override void UpdateAccessory(Player player, bool hideVisual) {
            player.accRunSpeed *= 1.5f; 
            player.maxRunSpeed *= 1.5f;
            player.wingTimeMax = (int)(player.wingTimeMax * 1.5f);
            player.GetDamage(DamageClass.Generic) *= 1.75f;
        }
    }
}