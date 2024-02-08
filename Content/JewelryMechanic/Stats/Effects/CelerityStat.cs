namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class CelerityStat : JewelStatEffect
{
    public override StatType Type => StatType.Celerity;
    public override Color Color => Color.Lime;

    public override void Apply(Player player, float strength)
    {
        var bonus = GetEffectBonus(player, strength) / 100f;
        player.GetModPlayer<CelerityPlayer>().bonus = bonus;
        player.wingTimeMax = (int)(player.wingTimeMax * (1 + bonus));
    }

    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.DexterityStrength * multiplier * 1.5f;

    class CelerityPlayer : ModPlayer
    {
        public float bonus = 0;

        public override void ResetEffects() => bonus = 0;

        public override void PostUpdateRunSpeeds()
        {
            Player.maxRunSpeed += bonus;
            Player.moveSpeed += bonus;
         }
    }
}
