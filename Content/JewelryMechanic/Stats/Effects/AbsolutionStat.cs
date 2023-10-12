namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class AbsolutionStat : JewelStatEffect
{
    public override StatType Type => StatType.Absolution;
    public override Color Color => new(135, 135, 135);

    public override StatExclusivity Exclusivity => StatExclusivity.Generic;

    public override void Apply(Player player, float strength) { }
    public override float GetEffectValue(float multiplier, Player player) => PeculiarJewelry.StatConfig.AbsolutionStrength * multiplier;
}
