using System;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class DiligenceStat : JewelStatEffect
{
    public override StatType Type => StatType.Diligence;
    public override Color Color => new(153, 102, 51);
    public override StatExclusivity Exclusivity => StatExclusivity.Utility;

    public override void Apply(Player player, float strength)
    {
        int bonus = (int)GetEffectBonus(player, strength);
        player.pickSpeed -= bonus / 1000f;
        player.GetModPlayer<DiligencePlayer>().diligenceBoost += bonus; // This benefits both pick range and pickup range
    }

    protected override float InternalEffectBonus(float multiplier, Player player) => (int)MathF.Ceiling(PeculiarJewelry.StatConfig.DiligenceStrength * multiplier * 4);

    class DiligenceItem : GlobalItem
    {
        public override void GrabRange(Item item, Player player, ref int grabRange)
        {
            int boost = player.GetModPlayer<DiligencePlayer>().diligenceBoost;
            
            if (boost > 0)
                grabRange += boost;
        }
    }

    class DiligencePlayer : ModPlayer
    {
        internal int diligenceBoost = 0;

        private int? _oldTileBoost = 0;

        public override void ResetEffects()
        {
            diligenceBoost = 0;
        }

        public override bool PreItemCheck()
        {
            if (diligenceBoost > 0 && Player.HeldItem.pick > 0)
            {
                _oldTileBoost = Player.HeldItem.tileBoost;
                Player.HeldItem.tileBoost = (int)(diligenceBoost / 20f);
            }

            return true;
        }

        public override void PostItemCheck()
        {
            if (_oldTileBoost is not null)
                Player.HeldItem.tileBoost = _oldTileBoost.Value;
        }
    }
}
