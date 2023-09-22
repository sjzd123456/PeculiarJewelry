using PeculiarJewelry.Content.JewelryMechanic.Misc;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelSupport;

public class CursedDollar : JewelSupportItem
{
    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(silver: 3);
        Item.Size = new(24, 28);
    }

    public override bool HardOverrideJewelCutChance(out float chance)
    {
        chance = 0.5f;
        return true;
    }
}
