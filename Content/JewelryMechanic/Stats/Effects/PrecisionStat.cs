namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

/// <summary>
/// Ranged damage. MP safe.
/// </summary>
internal class PrecisionStat : JewelStatEffect
{
    public override StatType Type => StatType.Precision;
    public override Color Color => Color.DarkGreen;

    public override StatExclusivity Exclusivity => StatExclusivity.Ranged;

    public override void Apply(Player player, float strength) => player.GetDamage(DamageClass.Ranged) += GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.PrecisionStrength * multiplier;
}
