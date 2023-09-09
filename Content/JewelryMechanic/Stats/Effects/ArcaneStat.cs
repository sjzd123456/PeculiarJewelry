namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class ArcaneStat : JewelStatEffect
{
    public override StatType Type => StatType.Arcane;
    public override Color Color => Color.Purple;

    public override StatExclusivity Exclusivity => StatExclusivity.Magic;

    public override void Apply(Player player, float strength, Item item) => player.statManaMax2 += (int)GetEffectValue(strength);
    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.ArcaneStrength * multiplier;
}
