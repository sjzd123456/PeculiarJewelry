namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

/// <summary>
/// Max mana. MP safe.
/// </summary>
internal class ArcaneStat : JewelStatEffect
{
    public override StatType Type => StatType.Arcane;
    public override Color Color => Color.Purple;

    public override StatExclusivity Exclusivity => StatExclusivity.Magic;

    public override void Apply(Player player, float strength) => player.statManaMax2 += (int)GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.ArcaneStrength * multiplier;
}
