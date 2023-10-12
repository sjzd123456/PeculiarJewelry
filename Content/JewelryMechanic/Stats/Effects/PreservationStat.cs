namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class PreservationStat : JewelStatEffect
{
    public override StatType Type => StatType.Preservation;
    public override Color Color => Color.LightGreen;

    public override StatExclusivity Exclusivity => StatExclusivity.Ranged;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier, Player player) => PeculiarJewelry.StatConfig.PreservationStrength * multiplier;
}
