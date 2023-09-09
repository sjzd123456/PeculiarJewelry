namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class CelerityStat : JewelStatEffect
{
    public override StatType Type => StatType.Celerity;
    public override Color Color => Color.Lime;

    public override void Apply(Player player, float strength, Item item)
    {
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.CelerityStrength * multiplier;
}
