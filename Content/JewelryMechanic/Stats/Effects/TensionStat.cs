namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class TensionStat : JewelStatEffect
{
    public override StatType Type => StatType.Tension;
    public override Color Color => Color.Green;

    public override StatExclusivity Exclusivity => StatExclusivity.Ranged;

    public override void Apply(Player player, float strength)
    {
    }

    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.TensionStrength * multiplier;
}
