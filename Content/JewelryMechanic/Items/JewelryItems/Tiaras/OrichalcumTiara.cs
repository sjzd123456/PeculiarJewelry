namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Tiaras;

[AutoloadEquip(EquipType.Head)]
public class OrichalcumTiara : BaseTiara
{
    public override string MaterialCategory => "Orichalcum";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.OrichalcumBar, 8)
            .AddTile(TileID.Anvils)
            .Register();
    }
}