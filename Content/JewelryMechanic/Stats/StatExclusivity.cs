namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

public enum StatExclusivity
{
    None,
    Melee,
    Ranged,
    Magic,
    Summon,
    Generic
}

public static class StatExclusivityLocalization
{
    public static string Localize(this StatExclusivity type) => Language.GetTextValue("Mods.PeculiarJewelry.Jewelry.StatExclusivities." + type);
}