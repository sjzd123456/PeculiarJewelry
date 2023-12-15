using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;

namespace PeculiarJewelry.Content.Buffs;

internal class CritTriggerBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex) 
        => player.GetCritChance(DamageClass.Generic) += player.GetModPlayer<StackableBuffTracker>().StackableStrength("Crit");
}
