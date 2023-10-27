namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Earrings;

[AutoloadEquip(EquipType.Face)]
public class SpectreEarring : BaseEarring
{
    public override string MaterialCategory => "Spectre";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.SpectreBar, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}