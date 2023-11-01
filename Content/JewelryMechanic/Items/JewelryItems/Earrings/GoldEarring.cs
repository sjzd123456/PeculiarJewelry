namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Earrings;

[AutoloadEquip(EquipType.Face)]
public class GoldEarring : BaseEarring
{
    public override string MaterialCategory => "Gold";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.GoldBar, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.Anvils)
            .Register();
    }
}