using Microsoft.CodeAnalysis.CSharp.Syntax;
using PeculiarJewelry.Content.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;

namespace PeculiarJewelry.Content.JewelryMechanic.Globals;

internal class JewelsInChestSystem : ModSystem
{
    public override void PostWorldGen()
    {
        for (int i = 0; i < Main.maxChests; i++)
        {
            if (Main.rand.NextBool(2))
                continue;

            var chest = Main.chest[i];

            if (chest is not null && Main.tile[chest.x, chest.y].TileType < TileID.Count && Main.tile[chest.x, chest.y].TileFrameX == 36)
                chest.AddItemToShop(GenerateJewelry());
        }
    }

    private static Item GenerateJewelry()
    {
        string[] materials = "Copper Tin Iron Lead Silver Tungsten Gold Platinum".Split(' ');
        string[] types = "Anklet Bracelet Brooch Choker Earring Hairpin Ring Tiara".Split(' ');
        string name = nameof(PeculiarJewelry) + "/" + Main.rand.Next(materials) + Main.rand.Next(types);
        int jewelryType = ModContent.Find<ModItem>(name).Type;
        Item item = new Item(jewelryType);
        BasicJewelry jewelry = item.ModItem as BasicJewelry;
        bool hasMajor = false;

        while (true && jewelry.Info.Count < 5)
        {
            if (Main.rand.NextFloat() < 0.25f)
                break;

            JewelInfo info = new MinorJewelInfo();

            if (!hasMajor && Main.rand.NextBool(6))
            {
                info = new MajorJewelInfo();
                hasMajor = true;
            }

            info.Setup((JewelTier)Main.rand.Next(3));
            jewelry.Info.Add(info);
        }

        return item;
    }
}
