using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;

internal class CopperBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Copper";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) => type == StatType.Vigor;
    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.15f;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1f;

    public override float EffectBonus(Player player, StatType statType)
    {
        int copperCount = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (copperCount >= 1)
            return bonus;
        return 1f;
    }

    public override void StaticBonus(Player player)
    {
        int copperCount = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (copperCount >= 3)
            player.statLifeMax2 += player.statDefense;
    }

    // Needs 5-Set
}
