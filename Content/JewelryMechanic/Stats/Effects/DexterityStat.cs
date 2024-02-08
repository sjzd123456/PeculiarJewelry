namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class DexterityStat : JewelStatEffect
{
    public override StatType Type => StatType.Dexterity;
    public override Color Color => Color.Olive;

    public override void Apply(Player player, float strength)
    {
        var bonus = GetEffectBonus(player, strength) / 200f;
        player.GetModPlayer<DexterityPlayer>().bonus = bonus;
        Player.jumpSpeed += bonus * 8;
    }

    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.DexterityStrength * multiplier;

    class DexterityPlayer : ModPlayer
    {
        public float bonus = 0;

        public override void ResetEffects() => bonus = 0;

        public override void PostUpdateRunSpeeds()
        {
            Player.runAcceleration += bonus;
            Player.wingRunAccelerationMult += bonus;
            Player.maxFallSpeed += bonus;
            Player.gravity += bonus / 2f;
        }
    }
    
    class DexterityItem : GlobalItem
    {
        public override void VerticalWingSpeeds(Item item, Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            var bonus = player.GetModPlayer<DexterityPlayer>().bonus;
            constantAscend += bonus;
        }

        public override void HorizontalWingSpeeds(Item item, Player player, ref float speed, ref float acceleration)
        {
            var bonus = player.GetModPlayer<DexterityPlayer>().bonus;
            acceleration += bonus;
            speed += bonus;
        }
    }
}
