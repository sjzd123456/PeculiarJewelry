namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers.TriggerEffects;

internal class SpawnProjTrigger : TriggerEffect
{
    public override TriggerType Type => TriggerType.InstantOther;

    protected override void InternalInstantEffect(TriggerContext context, Player player, float coefficient, JewelTier tier)
    {
        int damage = (int)TotalTriggerPower(player, coefficient);
        var vel = player.DirectionTo(Main.MouseWorld) * 12;
        Projectile.NewProjectile(player.GetSource_Misc("JewelryTrigger:" + context), player.Center, vel, ProjectileID.GoldCoin, damage, 2f, player.whoAmI);
    }

    public override float TriggerPower() => 40;
}
