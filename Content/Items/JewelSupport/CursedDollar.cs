using PeculiarJewelry.Content.JewelryMechanic.Misc;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System.Collections.Generic;

namespace PeculiarJewelry.Content.Items.JewelSupport;

public class CursedDollar : JewelSupportItem
{
    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(silver: 3);
        Item.Size = new(24, 28);
        Item.maxStack = Item.CommonMaxStack;
    }

    public override bool HardOverrideJewelCutChance(JewelInfo info, out float chance)
    {
        chance = 0.5f;
        return true;
    }
}
