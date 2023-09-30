using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using PeculiarJewelry.Content.JewelryMechanic.Stats.IO;
using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;

public class BasicJewelry : ModItem
{
    public enum JewelryTier : byte
    {
        Ordinary,
        Pretty,
        Elegant,
        Elaborant,
        Extravagant,
    }

    public List<JewelInfo> Info { get; private set; }
    public JewelryTier tier = JewelryTier.Ordinary;

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(silver: 3);

        tier = JewelryTier.Extravagant;
        Info = new((int)tier + 1);
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        if (!PeculiarJewelry.ShiftDown)
            tooltips.Add(new TooltipLine(Mod, "ShiftNotice", Language.GetTextValue("Mods.PeculiarJewelry.Jewelry.HoldShift")));

        foreach (var item in Info)
            Jewel.JewelInfoTooltips(tooltips, item, this, false);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        foreach (var item in Info)
        {
            if (item is MajorJewelInfo major)
                player.GetModPlayer<JewelPlayer>().majorInfo.Add(major);

            item.ApplyTo(player, Item);
        }
    }

    public override void SaveData(TagCompound tag)
    {
        tag.Add("jewelryJewelCount", (byte)Info.Count);

        for (int i = 0; i < Info.Count; ++i)
        {
            JewelInfo item = Info[i];
            tag.Add("jewelryInfo" + i, item.SaveAs());
        }
    }

    public override void LoadData(TagCompound tag)
    {
        byte count = tag.GetByte("jewelryJewelCount");

        for (int i = 0; i < count; ++i)
        {
            JewelInfo newInfo = JewelIO.LoadInfo(tag.GetCompound("jewelryInfo" + i));
            Info.Add(newInfo);
        }
    }
}