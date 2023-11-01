namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class MightStat : JewelStatEffect
{
    public override StatType Type => StatType.Might;
    public override Color Color => Color.Crimson;

    public override StatExclusivity Exclusivity => StatExclusivity.Melee;

    public override void Apply(Player player, float strength) => player.GetDamage(DamageClass.Melee) += GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.MightStrength * multiplier;
}
