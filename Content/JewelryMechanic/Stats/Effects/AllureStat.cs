using System;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

/// <summary>
/// Fishing power. MP safe.
/// </summary>
internal class AllureStat : JewelStatEffect
{
    public override StatType Type => StatType.Allure;
    public override Color Color => Color.PaleTurquoise;

    public override StatExclusivity Exclusivity => StatExclusivity.Utility;

    public override void Apply(Player player, float strength) => player.fishingSkill += (int)GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => (int)Math.Ceiling(PeculiarJewelry.StatConfig.AllureStrength * multiplier);
}
