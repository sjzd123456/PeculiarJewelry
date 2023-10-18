﻿using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;

internal class CobaltBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Cobalt";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) => type == StatType.Abundance || type == StatType.Legion || type == StatType.Order;
    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.15f;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1f;

    public override float EffectBonus(Player player, StatType statType)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 1)
            return bonus;
        return 1f;
    }

    public override void StaticBonus(Player player)
    {
        
    }

    // Needs 3-Set, 5-Set
}
