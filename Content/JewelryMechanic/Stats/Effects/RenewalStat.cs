using PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;
using Terraria;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class RenewalStat : JewelStatEffect
{
    public override StatType Type => StatType.Renewal;
    public override Color Color => Color.LightPink;

    public override void Apply(Player player, float strength) => player.lifeRegen += (int)GetEffectValue(strength, player);
    public override float GetEffectValue(float multiplier, Player player) 
        => PeculiarJewelry.StatConfig.RenewalStrength * multiplier * BaseMaterialBonus.BonusesByKey["Tin"].EffectBonus(player);
}
