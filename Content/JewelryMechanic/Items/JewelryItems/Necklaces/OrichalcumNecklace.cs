namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Necklaces;

[AutoloadEquip(EquipType.Neck)]
public class OrichalcumNecklace : BaseNecklace
{
    public override string MaterialCategory => "Orichalcum";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.OrichalcumBar, 6)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}