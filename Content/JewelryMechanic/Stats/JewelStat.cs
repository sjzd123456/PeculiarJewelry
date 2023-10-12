using PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;
using Terraria;

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

    public void Apply(Player player) => JewelStatEffect.StatsByType[Type].Apply(player, Strength);
    public float GetEffectValue(Player player, float add = 0f) => JewelStatEffect.StatsByType[Type].GetEffectValue(Strength + add, player);

    public JewelStatEffect Get() => JewelStatEffect.StatsByType[Type];
    public LocalizedText GetName() => JewelStatEffect.StatsByType[Type].DisplayName;

    public string GetDescription(Player player, bool showStars = true)
    {
        var stat = JewelStatEffect.StatsByType[Type];
        string stars = " ";

        if (showStars)
        {

            if (Strength > 1)
            {
                for (int i = 1; i < Strength - 1; ++i)
                    stars += "⋆";
            }
        }

        return stat.Description.WithFormatArgs(stat.GetEffectValue(Strength, player).ToString("#0.##")).Value + stars;
    }
}
