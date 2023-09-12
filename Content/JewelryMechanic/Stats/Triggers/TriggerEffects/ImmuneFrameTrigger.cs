namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers.TriggerEffects;

internal class ImmuneFrameTrigger : TriggerEffect
{
    public override TriggerType Type => TriggerType.InstantOther;
    public override bool NeedsCooldown => true;

    protected override void InternalInstantOtherEffect(TriggerContext context, Player player, float coefficient, JewelInfo.JewelTier tier)
    {
        int time = (int)TooltipArgument(coefficient, tier);
        player.AddImmuneTime(ImmunityCooldownID.General, time);
        player.AddImmuneTime(ImmunityCooldownID.Bosses, time);
        player.immuneNoBlink = false;
        player.immune = true;
        player.AddBuff(CooldownBuffType, CooldownTime(tier));
    }

    public override float TooltipArgument(float coefficient, JewelInfo.JewelTier tier) => 30 * coefficient;
}
