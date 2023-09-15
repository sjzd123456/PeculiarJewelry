using System;
using System.Collections.Generic;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

public abstract partial class JewelInfo
{
    public JewelStat Major { get; protected set; }
    public List<JewelStat> SubStats { get; protected set; } = null;

    public JewelTier tier = JewelTier.Natural;
    public StatExclusivity exclusivity = StatExclusivity.None;
    public int cuts = 0;
    public int successfulCuts = 0;

    public virtual int MaxCuts => 20 + (int)tier;

    public void Setup(JewelTier tier)
    {
        this.tier = tier;// (JewelTier)Main.rand.Next((int)JewelTier.Mutated0, (int)JewelTier.Stellar2 + 1);

        Major = JewelStat.Random;

        InternalSetup();

        if (SubStats is null)
            throw new NullReferenceException("SubStats shouldn't be null! Set them in InternalSetup.");

        RollSubstats();
    }

    internal void SetupFromIO(JewelStat major)
    {
        Major = major;
        InternalSetup();
    }

    public void RollSubstats()
    {
        exclusivity = Major.Get().Exclusivity;

        List<StatType> takenTypes = new() { Major.Get().Type };

        int spawnStats = 4; //Different tiers start with different sub stats

        if (tier == JewelTier.Natural)
            spawnStats = 0;
        else if (tier < JewelTier.Mythical0)
            spawnStats = 1;
        else if (tier < JewelTier.Celestial0)
            spawnStats = 2;
        else if (tier < JewelTier.Stellar0)
            spawnStats = 3;

        for (int i = 0; i < spawnStats; i++)
            AddSubStat(takenTypes, i);
    }

    private void AddSubStat(List<StatType> takenTypes, int index)
    {
        if (index < SubStats.Capacity) //Fill slots
        {
            SubStats.Add(JewelStat.Random);

            while ((SubStats[index].Get().Exclusivity != exclusivity && SubStats[index].Get().Exclusivity != StatExclusivity.None) || takenTypes.Contains(SubStats[index].Get().Type))
                SubStats[index] = JewelStat.Random;

            takenTypes.Add(SubStats[index].Get().Type);

            if (exclusivity == StatExclusivity.None)
                exclusivity = SubStats[index].Get().Exclusivity;
        }
        else
        {
            int adjI = index - SubStats.Capacity;
            SubStats[adjI].Strength++;
        }
    }

    public void ApplyTo(Player player, Item item)
    {
        Major.Apply(player, item);

        foreach (var subStat in SubStats)
            subStat.Apply(player, item);
    }

    public string[] SubStatTooltips()
    {
        string[] tooltip = new string[SubStats.Capacity];

        for (int i = 0; i < SubStats.Capacity; ++i)
        {
            if (i < SubStats.Count)
                tooltip[i] = SubStats[i].GetDescription();
            else
                tooltip[i] = "-";
        }

        return tooltip;
    }

    internal virtual void InternalSetup() { }

    public int GetDisplayTier()
    {
        return tier switch
        {
            JewelTier.Natural => 0,
            JewelTier.Mutated0 or JewelTier.Mutated1 or JewelTier.Mutated2 or JewelTier.Mutated3 or JewelTier.Mutated4 or JewelTier.Mutated5 => 1,
            JewelTier.Mythical0 or JewelTier.Mythical1 or JewelTier.Mythical2 or JewelTier.Mythical3 or JewelTier.Mythical4 or JewelTier.Mythical5 => 2,
            JewelTier.Celestial0 or JewelTier.Celestial1 or JewelTier.Celestial2 or JewelTier.Celestial3 or JewelTier.Celestial4 => 3,
            _ => 4
        };
    }

    public string GetTierText()
    {
        string key = "Mods.PeculiarJewelry.Jewelry.TierPrefixes.";
        return GetDisplayTier() switch
        {
            0 => Language.GetTextValue(key + "Natural"),
            1 => Language.GetTextValue(key + "Mutated"),
            2 => Language.GetTextValue(key + "Mythical"),
            3 => Language.GetTextValue(key + "Celestial"),
            _ => Language.GetTextValue(key + "Stellar")
        };
    }

    internal void TryAddCut(float chance)
    {
        cuts++;

        if (Main.rand.NextFloat() < chance)
            SuccessfulCut();
    }

    internal void SuccessfulCut()
    {
        successfulCuts++;
        Main.NewText($"Cuts: {cuts}/{MaxCuts}");

        Major.Strength += 1f;

        if (successfulCuts % 4 == 0)
        {
            if (SubStats.Count == SubStats.Capacity)
                Main.rand.Next(SubStats).Strength += 1f;
            else
            {
                List<StatType> takenTypes = new() { Major.Type };

                foreach (var item in SubStats)
                    takenTypes.Add(item.Type);

                AddSubStat(takenTypes, SubStats.Count);
            }
        }
    }
}
