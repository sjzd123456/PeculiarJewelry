using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using PeculiarJewelry.Content.JewelryMechanic.Stats.IO;
using System;
using System.Collections.Generic;
using Terraria;
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
        {
            SummaryJewelryTooltips(tooltips, Info, Mod);
            tooltips.Add(new TooltipLine(Mod, "ShiftNotice", Language.GetTextValue("Mods.PeculiarJewelry.Jewelry.HoldShift")));
        }
        else
            foreach (var item in Info)
                Jewel.PlainJewelTooltips(tooltips, item, this, false);
    }

    public static void SummaryJewelryTooltips(List<TooltipLine> tooltips, List<JewelInfo> info, Mod mod)
    {
        Dictionary<StatType, float> strengthsByType = new();
        Dictionary<StatType, Color> colorsByType = new();
        int triggerIndex = 0;

        foreach (var item in info)
        {
            List<JewelStat> stats = new() { item.Major };
            stats.AddRange(item.SubStats);

            foreach (var stat in stats)
            {
                if (strengthsByType.ContainsKey(stat.Type))
                    strengthsByType[stat.Type] += stat.GetEffectValue();
                else
                {
                    strengthsByType.Add(stat.Type, stat.GetEffectValue());
                    colorsByType.Add(stat.Type, stat.Get().Color);
                }
            }

            if (item is MajorJewelInfo major)
                tooltips.Add(new(mod, "TriggerEffect" + triggerIndex++, major.TriggerTooltip()));
        }

        foreach (var (type, strength) in strengthsByType)
        {
            var desc = "+" + Language.GetText("Mods.PeculiarJewelry.Jewelry.StatTypes." + type + ".Description").WithFormatArgs(strength.ToString("#0.##")).Value;
            tooltips.Add(new TooltipLine(mod, "SummaryInfo" + type, desc) { OverrideColor = colorsByType[type] });
        }
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