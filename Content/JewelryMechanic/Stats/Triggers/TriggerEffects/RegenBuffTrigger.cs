using PeculiarJewelry.Content.Buffs;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers.TriggerEffects;

internal class RegenTriggerConditional : TriggerEffect
{
    public override TriggerType Type => TriggerType.Conditional;

    protected override void InternalConditionalEffect(TriggerContext context, Player player, float coefficient, JewelTier tier)
    {
        player.GetModPlayer<StackableBuffTracker>().StackableBuff<RegenTriggerBuff>("Regen", new(2, ConditionalStrength(coefficient, tier)));
    }

    public override float TriggerPower() => 100;
}


internal class RegenTriggerInstant : TriggerEffect
{
    public override TriggerType Type => TriggerType.InstantStatus;

    protected override void InternalInstantEffect(TriggerContext context, Player player, float coefficient, JewelTier tier)
    {
        player.GetModPlayer<StackableBuffTracker>().StackableBuff<RegenTriggerBuff>("Regen", new((int)(coefficient * 5 * 60), TriggerPower() / 10f));
    }

    public override float TriggerPower() => 150f;
}
