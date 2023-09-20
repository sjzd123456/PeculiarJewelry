using PeculiarJewelry.Content.JewelryMechanic.Misc;
using PeculiarJewelry.Content.JewelryMechanic.NPCs;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;
using Terraria.DataStructures;
using Terraria.ObjectData;
using Verdant.Items;

namespace PeculiarJewelry.Content.JewelryMechanic.Items;

public class AncientCoffer : BagOfShinies
{
    public override void SetStaticDefaults() => ItemID.Sets.DisableAutomaticPlaceableDrop[Type] = true;

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<AncientCofferTile>());
        Item.Size = new(62, 46);
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.maxStack = 1;

        tier = (JewelTier)Main.rand.Next(Math.Max((int)BossLootGlobal.HighestBeatenTier - 4, 0), (int)BossLootGlobal.HighestBeatenTier + 1);
    }

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        base.ModifyItemLoot(itemLoot);

        itemLoot.AddCommon(ItemID.SilverCoin, 1, 4, 16);
        itemLoot.AddCommon(ItemID.GoldCoin, 2, 1, 3);

        int[] itemIDArray = new int[] { ItemID.Amethyst, ItemID.Topaz, ItemID.Sapphire, ItemID.Emerald, ItemID.Ruby, ItemID.Diamond };
        (int, int)[] itemStackArray = new (int, int)[] { (2, 6), (2, 6), (1, 4), (1, 4), (1, 2), (1, 2) };

        itemLoot.Add(new LootPoolDrop(itemStackArray, 2, 1, 1, itemIDArray));
    }

    public override void OnSpawn(IEntitySource source) { }
}

public class BrokenAncientCoffer : ModItem
{
    public override string Texture => base.Texture.Replace("Broken", "");

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<AncientCofferTile>());
        Item.Size = new(62, 46);
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.maxStack = 1;
    }
}

public class AncientCofferTile : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileTable[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
        TileObjectData.newTile.Width = 3;
        TileObjectData.newTile.Height = 2;
        TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
        TileObjectData.newTile.Origin = new Point16(1, 1);
        TileObjectData.addTile(Type);

        DustType = DustID.Stone;

        AddMapEntry(new Color(80, 91, 91));
        RegisterItemDrop(ModContent.ItemType<BrokenAncientCoffer>());
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;
}