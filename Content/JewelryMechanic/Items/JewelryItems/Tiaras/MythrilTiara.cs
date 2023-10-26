namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Tiaras;

[AutoloadEquip(EquipType.Head)]
public class MythrilTiara : BaseTiara
{
    public override string MaterialCategory => "Mythril";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.MythrilBar, 8)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}