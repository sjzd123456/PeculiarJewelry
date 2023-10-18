namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class WillpowerStat : JewelStatEffect
{
    public override StatType Type => StatType.Willpower;
    public override Color Color => Color.MediumPurple;

    public override StatExclusivity Exclusivity => StatExclusivity.Magic;

    public override void Apply(Player player, float strength) => player.GetDamage(DamageClass.Magic) += GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.WillpowerStat * multiplier;
}
