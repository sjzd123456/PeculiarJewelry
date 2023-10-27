namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Earrings;

[AutoloadEquip(EquipType.Face)]
public class LeadEarring : BaseEarring
{
    public override string MaterialCategory => "Lead";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.LeadBar, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}