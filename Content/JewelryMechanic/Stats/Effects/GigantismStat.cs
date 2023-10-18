namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class GigantismStat : JewelStatEffect
{
    public override StatType Type => StatType.Gigantism;
    public override Color Color => Color.IndianRed;

    public override StatExclusivity Exclusivity => StatExclusivity.Melee;

    public override void Apply(Player player, float strength)
    {
    }

    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.GigantismStrength * multiplier;
}
