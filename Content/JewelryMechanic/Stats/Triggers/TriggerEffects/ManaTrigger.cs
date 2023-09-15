namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers.TriggerEffects;

internal class ManaTrigger : TriggerEffect
{
    public override TriggerType Type => TriggerType.InstantOther;

    protected override void InternalInstantOtherEffect(TriggerContext context, Player player, float coefficient, JewelTier tier)
    {
        int mana = (int)TooltipArgument(coefficient, tier);

        if (player.statMana + mana > player.statManaMax2)
            mana = player.statMana + mana - player.statManaMax2;

        player.ManaEffect(mana);
        player.statMana += mana;
    }

    public override float TooltipArgument(float coefficient, JewelTier tier) => 20 * coefficient;
}
