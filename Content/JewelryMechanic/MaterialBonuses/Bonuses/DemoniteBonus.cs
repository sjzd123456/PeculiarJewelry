using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class DemoniteBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Demonite";

    private float damageBonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) => 
        type == StatType.Potency || type == StatType.Might || type == StatType.Order || type == StatType.Precision || type == StatType.Willpower || // Benefits
        type == StatType.Permenance || type == StatType.Tenacity; // Reduces

    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => damageBonus = 1.25f;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => damageBonus = 1f;

    public override float EffectBonus(Player player, StatType statType)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);
        
        if (count >= 1)
            return statType == StatType.Permenance || statType == StatType.Tenacity ? 0.94f : damageBonus;
        return 1f;
    }

    public override void StaticBonus(Player player)
    {

    }

    // Needs 3-Set, 5-Set
}
