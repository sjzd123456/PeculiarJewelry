namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class TenacityStat : JewelStatEffect
{
    public override StatType Type => StatType.Tenacity;
    public override Color Color => Color.SlateGray;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.TenacityStrength * multiplier;
}
