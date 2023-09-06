namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class PotencyStat : JewelStatEffect
{
    public override StatType Type => StatType.Potency;
    public override Color Color => Color.White;

    public override StatExclusivity Exclusivity => StatExclusivity.Generic;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.PotencyStrength * multiplier;
}
