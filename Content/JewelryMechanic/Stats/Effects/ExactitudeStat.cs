namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class ExactitudeStat : JewelStatEffect
{
    public override StatType Type => StatType.Exactitude;
    public override Color Color => Color.Yellow;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier, Player player) => PeculiarJewelry.StatConfig.ExactitudeStrength * multiplier;
}
