using PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

public class JewelStat
{
    public static JewelStat Random => new((StatType)Main.rand.Next((int)StatType.Max));

    public readonly StatType Type;

    public float Strength;

    public JewelStat(StatType category)
    {
        Type = category;
        Strength = JewelryCommon.StatStrengthRange();
    }

    public void Apply(Player player, Item item) => JewelStatEffect.StatsByType[Type].Apply(player, Strength, item);

    public JewelStatEffect Get() => JewelStatEffect.StatsByType[Type];
    public LocalizedText GetName() => JewelStatEffect.StatsByType[Type].DisplayName;

    public string GetDescription()
    {
        var stat = JewelStatEffect.StatsByType[Type];
        string stars = " ";

        if (Strength > 1)
        {
            for (int i = 0; i < Strength - 1; ++i)
                stars += "⋆";
        }

        return stat.Description.WithFormatArgs(stat.GetEffectValue(Strength).ToString("#0.##")).Value + stars;
    }
}
