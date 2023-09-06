using PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

public class JewelStat
{
    public static JewelStat Random => new((StatType)Main.rand.Next((int)StatType.Max));

    public readonly StatType Type;
    public readonly float Strength;

    public JewelStat(StatType category)
    {
        Type = category;
        Strength = JewelryCommon.StatStrengthRange();
    }

    public void Apply(Player player) => JewelStatEffect.StatsByType[Type].Apply(player, Strength);

    public JewelStatEffect Get() => JewelStatEffect.StatsByType[Type];
    public LocalizedText GetName() => JewelStatEffect.StatsByType[Type].DisplayName;

    public LocalizedText GetDescription()
    {
        var stat = JewelStatEffect.StatsByType[Type];
        return stat.Description.WithFormatArgs(stat.GetEffectValue(Strength).ToString("#0.##"));
    }
}
