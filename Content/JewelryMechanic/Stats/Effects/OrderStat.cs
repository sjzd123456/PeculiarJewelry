namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class OrderStat : JewelStatEffect
{
    public override StatType Type => StatType.Order;
    public override Color Color => Color.DarkTurquoise;

    public override StatExclusivity Exclusivity => StatExclusivity.Summon;

    public override void Apply(Player player, float strength) => player.GetDamage(DamageClass.Summon) += GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.OrderStrength * multiplier;
}
