using PeculiarJewelry.Content.JewelryMechanic.Misc;

namespace PeculiarJewelry.Content.Items.JewelSupport;

public class LuckyCoin : JewelSupportItem
{
    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(gold: 15);
        Item.Size = new(22);
        Item.maxStack = Item.CommonMaxStack;
    }
}
