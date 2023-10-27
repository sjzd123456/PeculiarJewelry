namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Earrings;

[AutoloadEquip(EquipType.Face)]
public class ShroomiteEarring : BaseEarring
{
    public override string MaterialCategory => "Shroomite";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.ShroomiteBar, 4)
            .AddIngredient(ItemID.Chain)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}