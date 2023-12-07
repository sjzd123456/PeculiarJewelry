using PeculiarJewelry.Content.JewelryMechanic.Misc;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.Items.JewelSupport;

public class GoldenCarpScales : JewelSupportItem
{
    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(gold: 5);
        Item.Size = new(30, 32);
    }

    public override bool CanBePlacedInSupportSlot(JewelInfo info) => info.cuts >= 16;
    public override float ModifyJewelCutChance(JewelInfo info, float baseChance) => baseChance + 0.08f;
}
