namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class PermenanceStat : JewelStatEffect
{
    public override StatType Type => StatType.Permenance;
    public override Color Color => Color.DarkGray;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.PermenanceStrength * multiplier;
}
