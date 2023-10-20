using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;

internal class ChlorophyteBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Chlorophyte";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) => true;
    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.15f;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1f;

    public override float EffectBonus(Player player, StatType type)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);
        bool defensive = type == StatType.Permenance || type == StatType.Tenacity || type == StatType.Vigor || type == StatType.Renewal;

        if (count >= 1)
            return defensive ? bonus : 0.95f;
        return 1f;
    }

    public override void StaticBonus(Player player)
    {
        
    }

    // Needs 3-Set, 5-Set
}
