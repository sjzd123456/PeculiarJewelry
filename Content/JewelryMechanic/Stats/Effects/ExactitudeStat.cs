namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

/// <summary>
/// Crit chance. MP safe.
/// </summary>
internal class ExactitudeStat : JewelStatEffect
{
    public override StatType Type => StatType.Exactitude;
    public override Color Color => Color.Yellow;

    public override void Apply(Player player, float strength) => player.GetCritChance(DamageClass.Generic) += GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.ExactitudeStrength * multiplier;
}
