using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;

public class MinorJewel : Jewel
{
    protected override Type InfoType => typeof(MinorJewelInfo);

    public sealed override void Defaults()
    {
        Item.width = 24;
        Item.height = 22;
        Item.rare = ItemRarityID.Green;
        Item.maxStack = 1;
    }
}
