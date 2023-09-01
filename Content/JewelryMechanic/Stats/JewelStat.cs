using PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

public class JewelStat
{
    public readonly StatCategory Type;

    public int upgrades;

    public JewelStat(StatCategory category)
    {
        Type = category;

        upgrades = 0;
    }

    public JewelStatEffect Get() => JewelStatEffect.StatByCategory[Type];
}
