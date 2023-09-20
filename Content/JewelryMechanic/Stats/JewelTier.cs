namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

public enum JewelTier : sbyte
{
    Invalid = -1,
    Natural = 0,
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
    Stellar0,
    Stellar1,
    Stellar2,
}

public static class JewelTierLocalization
{
    public static string Localize(this JewelTier tier)
    {
        string key = "Mods.PeculiarJewelry.Jewelry.TierPrefixes.";
        return tier.DisplayValue() switch
        {
            0 => Language.GetTextValue(key + "Natural"),
            1 => Language.GetTextValue(key + "Mutated"),
            2 => Language.GetTextValue(key + "Mythical"),
            3 => Language.GetTextValue(key + "Celestial"),
            _ => Language.GetTextValue(key + "Stellar")
        };
    }

    public static int DisplayValue(this JewelTier tier)
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
}