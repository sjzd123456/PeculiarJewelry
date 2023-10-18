namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Tiaras;

[AutoloadEquip(EquipType.Head)]
public class CrimtaneTiara : BaseTiara
{
    public override string MaterialCategory => "Crimtane";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.CrimtaneBar, 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}