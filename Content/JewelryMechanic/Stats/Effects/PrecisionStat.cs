namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class PrecisionStat : JewelStatEffect
{
    public override StatType Type => StatType.Precision;
    public override Color Color => Color.DarkGreen;

    public override StatExclusivity Exclusivity => StatExclusivity.Ranged;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.PrecisionStrength * multiplier;
}
