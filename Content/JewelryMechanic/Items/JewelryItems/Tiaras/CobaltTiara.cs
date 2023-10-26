namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Tiaras;

[AutoloadEquip(EquipType.Head)]
public class CobaltTiara : BaseTiara
{
    public override string MaterialCategory => "Cobalt";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.CobaltBar, 8)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}