namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class TenacityStat : JewelStatEffect
{
    public override StatType Type => StatType.Tenacity;
    public override Color Color => new(100, 100, 100);

    public override void Apply(Player player, float strength, Item item)
    {
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.TenacityStrength * multiplier;
}
