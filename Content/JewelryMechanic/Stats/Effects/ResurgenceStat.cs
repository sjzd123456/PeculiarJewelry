namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class ResurgenceStat : JewelStatEffect
{
    public override StatType Type => StatType.Resurgence;
    public override Color Color => Color.LavenderBlush;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.ResurgenceStrength * multiplier;
}
