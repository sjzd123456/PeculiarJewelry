using System.Collections.Generic;

namespace PeculiarJewelry.Content.Buffs;

internal class BuffSet : ModSystem
{
    public static HashSet<int> TriggerBuffs = [];

    public override void Unload() => TriggerBuffs = null;
    public override void Load() => TriggerBuffs = [];
}
