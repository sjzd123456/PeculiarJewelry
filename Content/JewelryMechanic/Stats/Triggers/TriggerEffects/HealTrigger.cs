namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers.TriggerEffects;

internal class HealTrigger : TriggerEffect
{
    public override TriggerType Type => TriggerType.InstantOther;
    public override bool NeedsCooldown => true;

    protected override void InternalInstantEffect(TriggerContext context, Player player, float coefficient, JewelTier tier)
    {
        int hp = (int)TotalTriggerPower(player, coefficient, tier);

        if (player.statLife + hp > player.statLifeMax2)
            hp = player.statLife + hp - player.statLifeMax2;

        player.Heal(hp);
        player.AddBuff(CooldownBuffType, CooldownTime(tier));
    }

    public override float TriggerPower() => 20;
}
