namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class PermenanceStat : JewelStatEffect
{
    public override StatType Type => StatType.Permenance;
    public override Color Color => new(70, 70, 70);

    public override void Apply(Player player, float strength, Item item) => player.statDefense += (int)GetEffectValue(strength);
    public override float GetEffectValue(float multiplier) => (int)(PeculiarJewelry.StatConfig.PermenanceStrength * multiplier);
}
