namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

/// <summary>
/// Max life. MP safe.
/// </summary>
internal class VigorStat : JewelStatEffect
{
    public override StatType Type => StatType.Vigor;
    public override Color Color => Color.DeepPink;

    public override void Apply(Player player, float strength) => player.statLifeMax2 += (int)GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.VigorStrength * multiplier;
}
