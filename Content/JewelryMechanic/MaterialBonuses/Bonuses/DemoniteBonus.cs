using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class DemoniteBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Demonite";

    private float damageBonus = 1f;
    private float bonusStrength = 1.25f;

    public override bool AppliesToStat(Player player, StatType type) => 
        type == StatType.Potency || type == StatType.Might || type == StatType.Order || type == StatType.Precision || type == StatType.Willpower || // Benefits
        type == StatType.Permenance || type == StatType.Tenacity; // Reduces

    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => damageBonus = bonusStrength;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => damageBonus = 1f;

    public override float EffectBonus(Player player, StatType statType)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);
        
        if (count >= 1)
            return statType == StatType.Permenance || statType == StatType.Tenacity ? 0.94f : damageBonus;
        return 1f;
    }

    public override void StaticBonus(Player player, bool firstSet)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);
        bonusStrength = 1.25f;

        if (count >= 3)
        {
            player.GetModPlayer<DemoniteBonusPlayer>().threeSet = true;
            bonusStrength = 1.4f;
        }
    }

    // Needs 5-Set

    class DemoniteBonusPlayer : ModPlayer
    {
        internal bool threeSet = false;

        public override void ResetEffects() => threeSet = false;

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (threeSet)
                modifiers.FinalDamage *= 1.4f;
        }
    }
}
