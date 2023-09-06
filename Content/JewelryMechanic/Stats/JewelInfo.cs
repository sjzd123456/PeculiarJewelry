using System;
using System.Collections.Generic;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

public abstract class JewelInfo
{
    public JewelStat Major { get; protected set; }
    public List<JewelStat> SubStats { get; protected set; } = null;

    public JewelTier tier = JewelTier.Natural;
    public StatExclusivity exclusivity = StatExclusivity.None;
    public int cuts = 0;

    public virtual int MaxCuts => 20 + (int)tier;

    public void Setup(JewelTier tier)
    {
        this.tier = tier;

        Major = JewelStat.Random;

        InternalSetup();

        if (SubStats is null)
            throw new NullReferenceException("SubStats shouldn't be null! Set them in InternalSetup.");

        RollSubstats();
    }

    public void RollSubstats()
    {
        exclusivity = Major.Get().Exclusivity;

        for (int i = 0; i < SubStats.Capacity; i++)
        {
            if (exclusivity == StatExclusivity.None)
                SubStats.Add(JewelStat.Random);
            else
            {
                SubStats.Add(JewelStat.Random);

                while (SubStats[i].Get().Exclusivity != exclusivity && SubStats[i].Get().Exclusivity != StatExclusivity.None)
                    SubStats[i] = JewelStat.Random;
            }
        }
    }

    public void ApplyTo(Player player)
    {
        Major.Apply(player);

        foreach (var item in SubStats)
            item.Apply(player);
    }

    public string[] SubStatTooltips()
    {
        string[] tooltip = new string[SubStats.Count];

        for (int i = 0; i < SubStats.Count; ++i)
            tooltip[i] = SubStats[i].GetDescription().Value;

        return tooltip;
    }

    internal virtual void InternalSetup() { }

    public enum JewelTier : byte
    {
        Natural,
        Mutated0,
        Mutated1,
        Mutated2,
        Mutated3,
        Mutated4,
        Mutated5,
        Mythical0,
        Mythical1,
        Mythical2,
        Mythical3,
        Mythical4,
        Mythical5,
        Celestial0,
        Celestial1,
        Celestial2,
        Celestial3,
        Celestial4,
        Stellar1,
        Stellar2,
        Stellar3,
    }

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
}
