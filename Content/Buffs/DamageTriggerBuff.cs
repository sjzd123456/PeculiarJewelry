using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;

namespace PeculiarJewelry.Content.Buffs;

internal class DamageTriggerBuff : ModBuff
{
    public override void SetStaticDefaults() => BuffSet.TriggerBuffs.Add(Type);

    public override void Update(Player player, ref int buffIndex) 
        => player.GetDamage(DamageClass.Generic) += player.GetModPlayer<StackableBuffTracker>().StackableStrength("Damage");

    public override void ModifyBuffText(ref string n, ref string t, ref int r) => t += StackableBuffTracker.GetBuffTooltips("Damage", "%", 100);
}
