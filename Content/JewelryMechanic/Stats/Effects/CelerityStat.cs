using PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class CelerityStat : JewelStatEffect
{
    public override StatType Type => StatType.Celerity;
    public override Color Color => Color.Lime;

    public override void Apply(Player player, float strength) => player.GetModPlayer<CelerityPlayer>().bonus = GetEffectValue(strength, player);
    public override float GetEffectValue(float multiplier, Player player)
         => PeculiarJewelry.StatConfig.DexterityStrength * multiplier * player.MaterialBonus("Silver", Type);

    class CelerityPlayer : ModPlayer
    {
        public float bonus = 0;

        public override void ResetEffects() => bonus = 0;

        public override void PostUpdateRunSpeeds()
        {
            Player.moveSpeed += bonus;
            Player.jumpSpeedBoost += bonus;
        }
    }
}
