using PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class VigorStat : JewelStatEffect
{
    public override StatType Type => StatType.Vigor;
    public override Color Color => Color.DeepPink;

    public override void Apply(Player player, float strength) => player.statLifeMax2 += (int)GetEffectValue(strength, player);
    public override float GetEffectValue(float multiplier, Player player) 
        => PeculiarJewelry.StatConfig.VigorStrength * multiplier * BaseMaterialBonus.BonusesByKey["Copper"].EffectBonus(player, Type);
}
