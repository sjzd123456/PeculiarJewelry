using PeculiarJewelry.Content.Buffs;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers.TriggerEffects;

internal class DodgeConditional : TriggerEffect
{
    public override TriggerType Type => TriggerType.Conditional;

    protected override void InternalConditionalEffect(TriggerContext context, Player player, float coefficient, JewelTier tier)
        => player.GetModPlayer<StackableBuffTracker>().StackableBuff<DodgeTriggerBuff>("Dodge", new(2, ConditionalStrength(coefficient, tier)));

    public override float TriggerPower() => 1;
}
