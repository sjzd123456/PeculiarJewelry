using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using Terraria.Initializers;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class SpectreBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Spectre";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) 
        => type == StatType.Celerity || type == StatType.Dexterity || type == StatType.Permenance || type == StatType.Tenacity;
    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.25f;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1f;

    public override float EffectBonus(Player player, StatType type)
    {
        int count = CountMaterial(player);
        bool defensive = type == StatType.Permenance || type == StatType.Tenacity;

        if (count >= 1)
            return defensive ? bonus : 0.94f;
        return 1f;
    }

    public override void StaticBonus(Player player, bool firstSet)
    {
        if (CountMaterial(player) >= 3)
        {
            player.waterWalk = player.waterWalk2 = true;

            if (player.wings <= 0) // This is the 3 set bonus - mimic wings
            {
                player.wings = ArmorIDs.Wing.JimsWings;
                player.wingTimeMax = player.GetWingStats(ArmorIDs.Wing.FishronWings).FlyTime;
                player.equippedWings = new Item(ItemID.FishronWings);
                player.wingsLogic = ArmorIDs.Wing.FishronWings;
            }
        }
    }

    // Needs 5-Set
}
