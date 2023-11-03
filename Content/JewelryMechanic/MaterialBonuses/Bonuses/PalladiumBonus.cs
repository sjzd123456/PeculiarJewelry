using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class PalladiumBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Palladium";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) => type == StatType.Frenzy || type == StatType.Gigantism || type == StatType.Might;
    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.15f;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1f;

    public override float EffectBonus(Player player, StatType statType)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 1)
            return bonus;
        return 1f;
    }

    public override void StaticBonus(Player player, bool firstSet)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 3)
            player.GetModPlayer<PalladiumBonusPlayer>().threeSet = true;
    }

    // Needs 5-Set

    class PalladiumBonusPlayer : ModPlayer
    {
        internal bool threeSet = false;

        private bool _speedUpNextSwing = false;

        public override void ResetEffects() => threeSet = false;

        public override float UseSpeedMultiplier(Item item)
        {
            if (_speedUpNextSwing)
            {
                _speedUpNextSwing = false;
                return 2.6f;
            }
            return 1f;
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (threeSet)
                _speedUpNextSwing = true;
        }
    }
}
