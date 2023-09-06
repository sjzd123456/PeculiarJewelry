namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class VigorStat : JewelStatEffect
{
    public override StatType Type => StatType.Vigor;
    public override Color Color => Color.DeepPink;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.VigorStrength * multiplier;
}
