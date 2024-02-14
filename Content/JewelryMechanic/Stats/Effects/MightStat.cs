namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

/// <summary>
/// Melee damage. MP safe.
/// </summary>
internal class MightStat : JewelStatEffect
{
    public override StatType Type => StatType.Might;
    public override Color Color => Color.Crimson;

    public override StatExclusivity Exclusivity => StatExclusivity.Melee;

    public override void Apply(Player player, float strength) => player.GetDamage(DamageClass.Melee) += GetEffectBonus(player, strength) / 100f;
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.MightStrength * multiplier;
}
