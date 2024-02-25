namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

/// <summary>
/// Summon damage. MP safe.
/// </summary>
internal class OrderStat : JewelStatEffect
{
    public override StatType Type => StatType.Order;
    public override Color Color => Color.DarkTurquoise;

    public override StatExclusivity Exclusivity => StatExclusivity.Summon;

    public override void Apply(Player player, float strength) => player.GetDamage(DamageClass.Summon) += GetEffectBonus(player, strength) / 100f;
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.OrderStrength * multiplier;
}
