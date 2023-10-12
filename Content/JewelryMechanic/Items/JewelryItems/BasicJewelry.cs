using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses;
using PeculiarJewelry.Content.JewelryMechanic.Misc;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using PeculiarJewelry.Content.JewelryMechanic.Stats.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using Terraria;
using Terraria.ModLoader.IO;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;

public abstract class BasicJewelry : ModItem
{
    public enum JewelryTier : byte
    {
        Ordinary,
        Pretty,
        Elegant,
        Elaborant,
        Extravagant,
    }

    public abstract string MaterialCategory { get; }

    public List<JewelInfo> Info { get; protected set; }
    public JewelryTier tier = JewelryTier.Ordinary;

    public sealed override void SetDefaults()
    {
        Item.accessory = true;
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(silver: 10);

        Defaults();

        tier = JewelryTier.Extravagant;
        Info = new((int)tier + 1);
    }

    protected virtual void Defaults() { }

    public sealed override void UpdateEquip(Player player)
    {
        foreach (var item in Info)
            player.GetModPlayer<JewelPlayer>().jewelInfos.Add(item);

        player.GetModPlayer<MaterialPlayer>().AddMaterial(MaterialCategory);
        EquipEffect(player);
    }

    protected virtual void EquipEffect(Player player) { }

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

    public static void SummaryJewelryTooltips(List<TooltipLine> tooltips, List<JewelInfo> info, Mod mod, Player player = null)
    {
        Dictionary<StatType, float> strengthsByType = new();
        Dictionary<StatType, Color> colorsByType = new();
        int triggerIndex = 0;
        player ??= Main.LocalPlayer;

        foreach (var item in info)
        {
            List<JewelStat> stats = new() { item.Major };
            stats.AddRange(item.SubStats);

            foreach (var stat in stats)
            {
                if (strengthsByType.ContainsKey(stat.Type))
                    strengthsByType[stat.Type] += stat.GetEffectValue(player);
                else
                {
                    strengthsByType.Add(stat.Type, stat.GetEffectValue(player));
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
            player.GetModPlayer<JewelPlayer>().jewelInfos.Add(item);
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