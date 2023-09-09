using System.Collections.Generic;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

internal class JewelPlayer : ModPlayer
{
    public List<MajorJewelInfo> majorInfo = new();

    public override void ResetEffects()
    {
        majorInfo.Clear();
    }

    public override void PostUpdateEquips()
    {
        foreach (var item in majorInfo)
            item.ConstantTrigger(Player, item.tier);
    }
}
