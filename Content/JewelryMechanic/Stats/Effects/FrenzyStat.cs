namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class FrenzyStat : JewelStatEffect
{
    public override StatType Type => StatType.Frenzy;
    public override Color Color => Color.OrangeRed;

    public override StatExclusivity Exclusivity => StatExclusivity.Melee;

    public override void Apply(Player player, float strength, Item item)
    {
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.FrenzyStrength * multiplier;
}
