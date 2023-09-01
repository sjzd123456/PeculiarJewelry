using Terraria;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

internal class MajorJewelInfo : JewelInfo
{
    public override void Setup()
    {
        Major = new JewelStat((StatCategory)Main.rand.Next((int)StatCategory.Max));
    }
}
