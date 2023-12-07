namespace PeculiarJewelry.Content.Items.JewelryItems.Necklaces;

[AutoloadEquip(EquipType.Neck)]
public class PalladiumNecklace : BaseNecklace
{
    public override string MaterialCategory => "Palladium";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.PalladiumBar, 6)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}