namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class OrderStat : JewelStatEffect
{
    public override StatType Type => StatType.Order;
    public override Color Color => Color.DarkTurquoise;

    public override StatExclusivity Exclusivity => StatExclusivity.Summon;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier, Player player) => PeculiarJewelry.StatConfig.OrderStrength * multiplier;
}
