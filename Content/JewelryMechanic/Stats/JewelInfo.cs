﻿using PeculiarJewelry.Content.Items.Jewels;
using System;
using System.Collections.Generic;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

public abstract partial class JewelInfo
{
    public abstract string Prefix { get; }
    public virtual int MaxCuts => 20 + (int)tier;

    public int RemainingCuts => MaxCuts - cuts;

    public string Name
    {
        get
        {
            string text = $"{Prefix} {tier.Localize()} {Jewel.Localize("Jewelry.Jewel")} of {Major.GetName().Value}";

            if (Major.Strength > 1)
                text += $" +{successfulCuts}";

            return text;
        }
    }

    public JewelStat Major { get; internal set; }
    public List<JewelStat> SubStats { get; protected set; } = null;

    public JewelTier tier = JewelTier.Natural;
    public StatExclusivity exclusivity = StatExclusivity.None;
    public int cuts = 0;
    public int successfulCuts = 0;

    public void Setup(JewelTier tier)
    {
        this.tier = tier;

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

        List<StatType> takenTypes = [Major.Get().Type];

        int spawnStats = tier switch
        {
            JewelTier.Natural => 0,
            < JewelTier.Mythical0 => 1,
            < JewelTier.Celestial0 => 2,
            < JewelTier.Stellar0 => 3,
            _ => 4
        }; //Different tiers start with different sub stats

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

    public void ApplyTo(Player player, float add = 0, float multiplier = 1f)
    {
        Major.Apply(player, add, multiplier);

        foreach (var subStat in SubStats)
            subStat.Apply(player, add, multiplier);
    }

    public string[] SubStatTooltips(Player player)
    {
        string[] tooltip = new string[SubStats.Capacity];

        for (int i = 0; i < SubStats.Capacity; ++i)
        {
            if (i < SubStats.Count)
                tooltip[i] = SubStats[i].GetDescription(player);
            else
                tooltip[i] = "-";
        }

        return tooltip;
    }

    internal virtual void InternalSetup() { }

    internal bool TryAddCut(float chance)
    {
        cuts++;

        if (Main.rand.NextFloat() < chance)
        {
            SuccessfulCut();
            return true;
        }
        return false;
    }

    internal void SuccessfulCut()
    {
        successfulCuts++;
        Major.Strength += JewelryCommon.StatStrengthRange();

        if (successfulCuts % 4 == 0)
        {
            if (SubStats.Count == SubStats.Capacity)
                Main.rand.Next(SubStats).Strength += JewelryCommon.StatStrengthRange();
            else
            {
                List<StatType> takenTypes = [Major.Type];

                foreach (var item in SubStats)
                    takenTypes.Add(item.Type);

                AddSubStat(takenTypes, SubStats.Count);
            }
        }
    }

    public bool InThresholdCut() => (cuts - 7) % 8 == 0;
}
