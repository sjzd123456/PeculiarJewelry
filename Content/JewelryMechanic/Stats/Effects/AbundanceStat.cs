using System;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class AbundanceStat : JewelStatEffect
{
    public override StatType Type => StatType.Abundance;
    public override Color Color => Color.LightCyan;

    public override StatExclusivity Exclusivity => StatExclusivity.Summon;

    public override void Apply(Player player, float strength, Item item) => player.maxMinions += (int)GetEffectValue(strength);
    public override float GetEffectValue(float multiplier) => (int)Math.Ceiling(PeculiarJewelry.StatConfig.AbundanceStrength * multiplier) * 2;
}
