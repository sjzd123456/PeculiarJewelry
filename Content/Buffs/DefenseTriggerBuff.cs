using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;

namespace PeculiarJewelry.Content.Buffs;

internal class DefenseTriggerBuff : ModBuff
{
    public override void SetStaticDefaults() => BuffSet.TriggerBuffs.Add(Type);

    public override void Update(Player player, ref int buffIndex) 
        => player.statDefense += (int)player.GetModPlayer<StackableBuffTracker>().StackableStrength("Defense");
}
