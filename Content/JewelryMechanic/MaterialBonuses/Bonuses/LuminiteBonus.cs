namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class LuminiteBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Luminite";

    public override int GetMajorJewelCount => 2;

    public override void StaticBonus(Player player)
    {
    }

    // Needs 3-Set, 5-Set
}
