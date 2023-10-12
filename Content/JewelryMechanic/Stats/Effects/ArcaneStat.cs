namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class ArcaneStat : JewelStatEffect
{
    public override StatType Type => StatType.Arcane;
    public override Color Color => Color.Purple;

    public override StatExclusivity Exclusivity => StatExclusivity.Magic;

    public override void Apply(Player player, float strength) => player.statManaMax2 += (int)GetEffectValue(strength, player);
    public override float GetEffectValue(float multiplier, Player player) => PeculiarJewelry.StatConfig.ArcaneStrength * multiplier;
}
