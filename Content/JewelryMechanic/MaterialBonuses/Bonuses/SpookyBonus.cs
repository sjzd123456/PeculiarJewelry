using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class SpookyBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Spooky";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) 
        => type == StatType.Exploitation || type == StatType.Exactitude || type == StatType.Renewal || type == StatType.Vigor;
    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.25f;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1f;

    public override float EffectBonus(Player player, StatType type)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);
        bool movement = type == StatType.Exploitation || type == StatType.Exactitude;

        if (count >= 1)
            return movement ? bonus : 0.94f;
        return 1f;
    }

    public override void StaticBonus(Player player, bool firstSet)
    {
        
    }

    // Needs 3-Set, 5-Set
}
