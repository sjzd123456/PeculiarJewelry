namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Necklaces;

[AutoloadEquip(EquipType.Neck)]
public class LuminiteNecklace : BaseNecklace
{
    public override string MaterialCategory => "Luminite";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.LunarBar, 6)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}