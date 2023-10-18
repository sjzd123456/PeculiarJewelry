namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class CelerityStat : JewelStatEffect
{
    public override StatType Type => StatType.Celerity;
    public override Color Color => Color.Lime;

    public override void Apply(Player player, float strength) => player.GetModPlayer<CelerityPlayer>().bonus = GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.DexterityStrength * multiplier;

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
