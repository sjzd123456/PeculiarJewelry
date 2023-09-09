using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System.Collections.Generic;
using System.ComponentModel;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;

class BasicJewelry : ModItem
{
    private readonly List<JewelInfo> info = new(5);

    public override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(silver: 3);
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        if (!PeculiarJewelry.ShiftDown)
            tooltips.Add(new TooltipLine(Mod, "ShiftNotice", Language.GetTextValue("Mods.PeculiarJewelry.Jewelry.HoldShift")));

        foreach (var item in info)
            Jewel.JewelInfoTooltips(tooltips, item, this, false);
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        foreach (var item in info)
        {
            if (item is MajorJewelInfo major)
                player.GetModPlayer<JewelPlayer>().majorInfo.Add(major);

            item.ApplyTo(player, Item);
        }

        if (info.Count < info.Capacity)
        {
            if (player.HeldItem.ModItem is not Jewel jewel)
                return;

            bool hasMajor = false;

            foreach (var item in info)
                if (item is MajorJewelInfo)
                    hasMajor = true;

            if (hasMajor && jewel is MajorJewel)
                return;

            info.Add(jewel.info);
            player.inventory[player.selectedItem] = new Item(ItemID.None);
            player.HeldItem.TurnToAir();

            return;
        }
    }
}