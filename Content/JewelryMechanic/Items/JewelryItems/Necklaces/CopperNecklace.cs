namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Necklaces;

[AutoloadEquip(EquipType.Neck)]
public class CopperNecklace : BaseNecklace
{
    public override string MaterialCategory => "Copper";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.CopperBar, 6)
            .AddTile(TileID.Anvils)
            .Register();
    }
}