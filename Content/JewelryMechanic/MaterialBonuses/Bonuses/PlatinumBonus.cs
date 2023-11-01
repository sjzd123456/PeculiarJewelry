using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class PlatinumBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Platinum";

    private float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) =>
        type == StatType.Diligence || type == StatType.Tolerance || type == StatType.Allure;

    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.25f;
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

    // Needs 1-Set, 3-Set, 5-Set
}
