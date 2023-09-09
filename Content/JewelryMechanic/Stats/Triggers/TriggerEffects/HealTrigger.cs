namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers.TriggerEffects;

internal class HealTrigger : TriggerEffect
{
    public override TriggerType Type => TriggerType.InstantOther;

    protected override void InternalInstantOtherEffect(TriggerContext context, Player player, float coefficient) => player.Heal((int)(20 * coefficient));
    public override string TooltipArgument(float coefficient, JewelInfo.JewelTier tier) => (20 * coefficient).ToString("#0.##");
}
