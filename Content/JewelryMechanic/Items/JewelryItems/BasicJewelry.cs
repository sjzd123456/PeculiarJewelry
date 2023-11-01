using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses;
using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;
using PeculiarJewelry.Content.JewelryMechanic.Misc;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using PeculiarJewelry.Content.JewelryMechanic.Stats.IO;
using System.Collections.Generic;
using System.Linq;
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
        
        tier = JewelryTier.Extravagant;
        Info = new((int)tier + 1);

        Defaults();
    }

    protected virtual void Defaults() { }

    public sealed override void UpdateEquip(Player player)
    {
        player.GetModPlayer<JewelPlayer>().jewelry.Add(this);
        player.GetModPlayer<MaterialPlayer>().AddMaterial(MaterialCategory);
        EquipEffect(player);
    }

    public override void EquipFrameEffects(Player player, EquipType type) => EquipEffect(player, true);
    public override void UpdateVanity(Player player) => EquipEffect(player, true);
    protected virtual void EquipEffect(Player player, bool isVanity = false) { }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var name = tooltips.First(x => x.Name == "ItemName");

        if (Info.Any())
        {
            var stat = DetermineHighestStat(Info);
            name.Text = $"{JewelryPrefix(tier)} {name.Text} of {stat.Localize()}";
        }
        else
            name.Text = $"{JewelryPrefix(tier)} {name.Text}";

        int count = Main.LocalPlayer.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialCategory);

        if (count >= 3)
            tooltips.Add(new TooltipLine(Mod, "3Set", Language.GetTextValue("Mods.PeculiarJewelry.Material.Bonuses." + MaterialCategory + ".3Set")));

        if (count >= 5)
            tooltips.Add(new TooltipLine(Mod, "5Set", Language.GetTextValue("Mods.PeculiarJewelry.Material.Bonuses." + MaterialCategory + ".5Set")));

        if (!PeculiarJewelry.ShiftDown)
        {
            SummaryJewelryTooltips(tooltips, Info, Mod);
            tooltips.Add(new TooltipLine(Mod, "ShiftNotice", Language.GetTextValue("Mods.PeculiarJewelry.Jewelry.HoldShift")));
        }
        else
            foreach (var item in Info)
                Jewel.PlainJewelTooltips(tooltips, item, this, false);
    }

    private static StatType DetermineHighestStat(List<JewelInfo> info)
    {
        Dictionary<StatType, int> typesByUpgrades = new();

        if (info.Count == 0)
            return StatType.Potency;

        foreach (var jewel in info)
        {
            List<JewelStat> stats = new() { jewel.Major };
            stats.AddRange(jewel.SubStats);

            foreach (var stat in stats)
            {
                if (typesByUpgrades.ContainsKey(stat.Get().Type))
                    typesByUpgrades[stat.Get().Type]++;
                else
                    typesByUpgrades.Add(stat.Get().Type, 1);
            }
        }

        int max = 0;
        StatType key = StatType.Willpower;

        foreach (var item in typesByUpgrades.Keys)
        {
            if (max < typesByUpgrades[item])
            {
                max = typesByUpgrades[item];
                key = item;
            }
        }

        return key;
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
                tooltips.Add(new(mod, "TriggerEffect" + triggerIndex++, major.TriggerTooltip(player)));
        }

        foreach (var (type, strength) in strengthsByType)
        {
            var desc = "+" + Language.GetText("Mods.PeculiarJewelry.Jewelry.StatTypes." + type + ".Description").WithFormatArgs(strength.ToString("#0.##")).Value;
            tooltips.Add(new TooltipLine(mod, "SummaryInfo" + type, desc) { OverrideColor = colorsByType[type] });
        }
    }

    public static string JewelryPrefix(JewelryTier tier) => Language.GetTextValue("Mods.PeculiarJewelry.Jewelry.JewelryPrefixes." + tier);

    public override void UpdateAccessory(Player player, bool hideVisual) => player.GetModPlayer<JewelPlayer>().jewelry.Add(this);

    public override bool CanReforge() => false;

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

    internal void ApplyTo(Player player, float add = 0, float multiplier = 1f)
    {
        foreach (var item in Info)
            item.ApplyTo(player, add, multiplier);
    }

    internal void ApplySingleJewelBonus(Player player) => player.SingleBonus(MaterialCategory, this);
    internal void ResetSingleJewelBonus(Player player) => player.UndoSingle(MaterialCategory, this);
    internal int MaxMajorCount() => BaseMaterialBonus.BonusesByKey[MaterialCategory].GetMajorJewelCount;

    public Color GetDisplayColor()
    {
        var jewel = Info.FirstOrDefault(x => x is MajorJewelInfo);
        jewel ??= Info.First();

        return jewel.Major.Get().Color;
    }
}