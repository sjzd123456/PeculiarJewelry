namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class RenewalStat : JewelStatEffect
{
    public override StatType Type => StatType.Renewal;
    public override Color Color => Color.LightPink;

    public override void Apply(Player player, float strength, Item item) => player.lifeRegen += (int)GetEffectValue(strength);
    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.RenewalStrength * multiplier;
}
