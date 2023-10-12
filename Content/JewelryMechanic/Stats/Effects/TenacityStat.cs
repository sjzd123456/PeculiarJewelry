using PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class TenacityStat : JewelStatEffect
{
    public override StatType Type => StatType.Tenacity;
    public override Color Color => new(100, 100, 100);

    public override void Apply(Player player, float strength) => player.endurance += GetEffectValue(strength, player);
    public override float GetEffectValue(float multiplier, Player player) 
        => PeculiarJewelry.StatConfig.TenacityStrength * multiplier * BaseMaterialBonus.BonusesByKey["Lead"].EffectBonus(player);
}
