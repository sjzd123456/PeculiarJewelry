namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Tiaras;

[AutoloadEquip(EquipType.Head)]
public class BeetleTiara : BaseTiara
{
    public override string MaterialCategory => "Beetle";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.BeetleHusk, 8)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}