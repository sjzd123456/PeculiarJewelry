using System.Collections.Generic;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

internal class MinorJewelInfo : JewelInfo
{
    public override string Prefix => "Minor";
    public override int MaxCuts => 10 + (int)((int)tier / 2f);

    internal override void InternalSetup()
    {
        SubStats = new List<JewelStat>(2);
    }
}
