namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Necklaces;

[AutoloadEquip(EquipType.Neck)]
public class CrimtaneNecklace : BaseNecklace
{
    public override string MaterialCategory => "Crimtane";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.CrimtaneBar, 6)
            .AddTile(TileID.Anvils)
            .Register();
    }
}