using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class RenewalStat : JewelStatEffect
{
    public override StatType Type => StatType.Renewal;
    public override Color Color => Color.LightPink;

    public override void Apply(Player player, float strength) => player.GetModPlayer<RenewalPlayer>().lifeRegen += (int)GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.RenewalStrength * multiplier;

    private class RenewalPlayer : ModPlayer
    {
        internal int lifeRegen = 0;

        public override void ResetEffects() => lifeRegen = 0;

        public override void UpdateLifeRegen()
        {
            Player.lifeRegen += lifeRegen;
            Player.GetModPlayer<TinBonus.TinMaterialPlayer>().TinBonuses();
        }
    }
}
