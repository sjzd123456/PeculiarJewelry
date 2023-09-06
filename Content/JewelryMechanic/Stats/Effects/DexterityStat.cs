namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class DexterityStat : JewelStatEffect
{
    public override StatType Type => StatType.Dexterity;
    public override Color Color => Color.Olive;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.DexterityStrength * multiplier;
}
