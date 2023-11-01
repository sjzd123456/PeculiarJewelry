using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class OrichalcumBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Orichalcum";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) => type == StatType.Arcane || type == StatType.Resurgence || type == StatType.Willpower;
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
        
    }

    // Needs 3-Set, 5-Set
}
