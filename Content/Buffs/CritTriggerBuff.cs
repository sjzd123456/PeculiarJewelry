using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;

namespace PeculiarJewelry.Content.Buffs;

internal class CritTriggerBuff : ModBuff
{
    public override void SetStaticDefaults() => BuffSet.TriggerBuffs.Add(Type);

    public override void Update(Player player, ref int buffIndex) 
        => player.GetCritChance(DamageClass.Generic) += player.GetModPlayer<StackableBuffTracker>().StackableStrength("Crit");

    public override void ModifyBuffText(ref string n, ref string tip, ref int r) => tip += StackableBuffTracker.GetBuffTooltips("Crit", "%", 1);
}
