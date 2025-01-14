using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;

namespace PeculiarJewelry.Content.Items.Jewels;

public class MinorJewel : Jewel
{
    protected override Type InfoType => typeof(MinorJewelInfo);
    protected override byte MaxVariations => 5;

    public sealed override void Defaults()
    {
        Item.width = 24;
        Item.height = 22;
        Item.rare = ItemRarityID.Green;
        Item.maxStack = 1;
        Item.value = Item.buyPrice(0, 10, 0, 0);
    }
}
