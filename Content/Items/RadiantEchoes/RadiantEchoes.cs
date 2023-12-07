using PeculiarJewelry.Content.JewelryMechanic.Misc;

namespace PeculiarJewelry.Content.Items.RadiantEchoes;

public class RadiantEcho : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(silver: 3);
        Item.Size = new(24, 28);
        Item.maxStack = Item.CommonMaxStack;
    }
}

public class DoubleRadiantEcho : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(silver: 3);
        Item.Size = new(28, 34);
        Item.maxStack = Item.CommonMaxStack;
    }
}

public class TripleRadiantEcho : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(silver: 3);
        Item.Size = new(28, 38);
        Item.maxStack = Item.CommonMaxStack;
    }
}

public class QuadRadiantEcho : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(silver: 3);
        Item.Size = new(28, 38);
        Item.maxStack = Item.CommonMaxStack;
    }
}

public class QuintRadiantEcho : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(silver: 3);
        Item.Size = new(40, 42);
        Item.maxStack = Item.CommonMaxStack;
    }
}