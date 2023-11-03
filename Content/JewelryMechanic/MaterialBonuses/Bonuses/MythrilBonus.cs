using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class MythrilBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Mythril";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) => type == StatType.Precision || type == StatType.Preservation || type == StatType.Tension;
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
            player.GetModPlayer<MythrilBonusPlayer>().threeSet = true;
    }

    // Needs 5-Set

    class MythrilBonusPlayer : ModPlayer
    {
        internal bool threeSet = false;

        public override void ResetEffects() => threeSet = false;
        public override float UseSpeedMultiplier(Item item) 
            => item.DamageType.CountsAsClass(DamageClass.Ranged) && threeSet && Player.velocity.LengthSquared() <= 0.0001f ? 2f : 1;
    }
}
