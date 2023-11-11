namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Necklaces;

[AutoloadEquip(EquipType.Neck)]
public class SpectreNecklace : BaseNecklace
{
    public override string MaterialCategory => "Spectre";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.SpectreBar, 6)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}