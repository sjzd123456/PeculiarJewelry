namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class AbundanceStat : JewelStatEffect
{
    public override StatType Type => StatType.Abundance;
    public override Color Color => Color.LightCyan;

    public override StatExclusivity Exclusivity => StatExclusivity.Summon;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.AbundanceStrength * multiplier;
}
