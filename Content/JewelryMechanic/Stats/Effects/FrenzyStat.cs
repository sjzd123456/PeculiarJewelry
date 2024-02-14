using System;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class FrenzyStat : JewelStatEffect
{
    public override StatType Type => StatType.Frenzy;
    public override Color Color => Color.OrangeRed;

    public override StatExclusivity Exclusivity => StatExclusivity.Melee;

    public override void Apply(Player player, float strength) => player.GetModPlayer<FrenzyPlayer>().bonus += GetEffectBonus(player, strength) / 100f;
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.FrenzyStrength * multiplier;

    class FrenzyPlayer : ModPlayer
    {
        private float RealBonus => 1 / (bonus + 1f);

        public float bonus = 0f;

        public override void ResetEffects() => bonus = 0f;
        public override float UseTimeMultiplier(Item item) => RealBonus;
        public override float UseAnimationMultiplier(Item item) => RealBonus;
    }
}
