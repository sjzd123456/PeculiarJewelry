using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;

public class MinorJewel : ModItem
{
    public sealed override void SetDefaults()
    {
        Item.width = 22;
        Item.height = 22;
        Item.rare = ItemRarityID.Green;
        Item.maxStack = 1;
    }
}
