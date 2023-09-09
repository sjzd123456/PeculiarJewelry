namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class LegionStat : JewelStatEffect
{
    public override StatType Type => StatType.Legion;
    public override Color Color => Color.CornflowerBlue;

    public override StatExclusivity Exclusivity => StatExclusivity.Summon;

    public override void Apply(Player player, float strength, Item item)
    {
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.LegionStrength * multiplier;
}
