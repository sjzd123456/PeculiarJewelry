namespace PeculiarJewelry.Content.JewelryMechanic.Items;

public class SparklyDust : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(silver: 3);
        Item.Size = new(28, 22);
        Item.maxStack = Item.CommonMaxStack;
    }
}
