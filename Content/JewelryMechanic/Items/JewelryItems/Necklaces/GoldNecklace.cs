namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Necklaces;

[AutoloadEquip(EquipType.Neck)]
public class GoldNecklace : BaseNecklace
{
    public override string MaterialCategory => "Gold";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.GoldBar, 6)
            .AddTile(TileID.Anvils)
            .Register();
    }
}