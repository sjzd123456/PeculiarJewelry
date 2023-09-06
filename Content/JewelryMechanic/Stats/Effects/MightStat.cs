namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class MightStat : JewelStatEffect
{
    public override StatType Type => StatType.Might;
    public override Color Color => Color.Crimson;

    public override StatExclusivity Exclusivity => StatExclusivity.Melee;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.MightStrength * multiplier;
}
