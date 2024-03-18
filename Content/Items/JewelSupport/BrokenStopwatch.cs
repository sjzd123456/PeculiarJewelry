using PeculiarJewelry.Content.JewelryMechanic.Misc;

namespace PeculiarJewelry.Content.Items.JewelSupport;

public class BrokenStopwatch : ModItem, ISetSupportItem
{
    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.buyPrice(gold: 25);
        Item.Size = new(32, 38);
    }
}
