using Terraria.Localization;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

public abstract class JewelInfo
{
    public JewelStat Major { get; protected set; }

    public JewelTier tier = JewelTier.Natural;
    public int cuts = 0;
    public int maxCuts = 1;

    public abstract void Setup();

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
