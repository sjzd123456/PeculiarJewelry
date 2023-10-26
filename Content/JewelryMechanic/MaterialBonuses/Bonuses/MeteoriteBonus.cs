namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class MeteoriteBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Meteorite";

    public override void StaticBonus(Player player)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

    }

    // Needs all
}
