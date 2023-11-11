namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Necklaces;

[AutoloadEquip(EquipType.Neck)]
public class ShroomiteNecklace : BaseNecklace
{
    public override string MaterialCategory => "Shroomite";

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.ShroomiteBar, 6)
            .AddTile(TileID.MythrilAnvil)
            .Register();
    }
}