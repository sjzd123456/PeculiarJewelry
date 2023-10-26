﻿using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class DexterityStat : JewelStatEffect
{
    public override StatType Type => StatType.Dexterity;
    public override Color Color => Color.Olive;

    public override void Apply(Player player, float strength) => player.GetModPlayer<DexterityPlayer>().bonus = GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.DexterityStrength * multiplier;

    class DexterityPlayer : ModPlayer
    {
        public float bonus = 0;

        public override void ResetEffects() => bonus = 0;

        public override void PostUpdateRunSpeeds()
        {
            Player.runAcceleration += bonus;
            Player.wingRunAccelerationMult += bonus;
        }
    }
}
