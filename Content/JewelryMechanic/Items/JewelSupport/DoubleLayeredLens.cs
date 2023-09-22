using PeculiarJewelry.Content.JewelryMechanic.Misc;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelSupport;

public class DoubleLayeredLens : JewelSupportItem
{
    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(silver: 3);
        Item.Size = new(24, 28);
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.IronBar, 3)
            .AddIngredient(ItemID.Lens, 2)
            .AddTile(TileID.Anvils)
            .Register();
    }

    public override float ModifyJewelCutChance(float baseChance) => baseChance + 0.05f;
}
