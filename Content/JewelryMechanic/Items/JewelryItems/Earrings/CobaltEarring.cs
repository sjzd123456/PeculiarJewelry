namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Earrings;

[AutoloadEquip(EquipType.Face)]
public class CobaltEarring : BaseEarring
{
    public override string MaterialCategory => "Cobalt";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.CobaltBar, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}