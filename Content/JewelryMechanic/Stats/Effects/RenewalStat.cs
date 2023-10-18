namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class RenewalStat : JewelStatEffect
{
    public override StatType Type => StatType.Renewal;
    public override Color Color => Color.LightPink;

    public override void Apply(Player player, float strength) => player.lifeRegen += (int)GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.RenewalStrength * multiplier;
}
