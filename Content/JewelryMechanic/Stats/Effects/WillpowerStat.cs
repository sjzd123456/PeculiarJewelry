namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class WillpowerStat : JewelStatEffect
{
    public override StatType Type => StatType.Willpower;
    public override Color Color => Color.MediumPurple;

    public override StatExclusivity Exclusivity => StatExclusivity.Magic;

    public override void Apply(Player player, float strength, Item item)
    {
        player.GetDamage(DamageClass.Magic) += GetEffectValue(strength);
    }

    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.WillpowerStat * multiplier;
}
