namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Tiaras;

[AutoloadEquip(EquipType.Head)]
public class SilverTiara : BaseTiara
{
    public override string MaterialCategory => "Silver";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.SilverBar, 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}