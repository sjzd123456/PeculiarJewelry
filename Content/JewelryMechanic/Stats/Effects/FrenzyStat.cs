namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class FrenzyStat : JewelStatEffect
{
    public override StatType Type => StatType.Frenzy;
    public override Color Color => Color.OrangeRed;

    public override StatExclusivity Exclusivity => StatExclusivity.Melee;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier, Player player) => PeculiarJewelry.StatConfig.FrenzyStrength * multiplier;
}
