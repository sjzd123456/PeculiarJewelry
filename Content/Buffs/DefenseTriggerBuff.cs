using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;
using System;

namespace PeculiarJewelry.Content.Buffs;

internal class DefenseTriggerBuff : ModBuff
{
    public override void SetStaticDefaults() => BuffSet.TriggerBuffs.Add(Type);

    public override void Update(Player player, ref int buffIndex) 
        => player.statDefense += (int)MathF.Ceiling(player.GetModPlayer<StackableBuffTracker>().StackableStrength("Defense") * 5);

    public override void ModifyBuffText(ref string n, ref string tip, ref int r) => tip += StackableBuffTracker.GetBuffTooltips("Defense", "", 5);
}
