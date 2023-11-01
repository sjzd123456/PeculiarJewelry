using PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;
using System.Threading;
using Terraria;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

public class JewelStat
{
    public static JewelStat Random => new(StatType.Permenance);//new((StatType)Main.rand.Next((int)StatType.Max));

    public readonly StatType Type;

    public float Strength;

    public JewelStat(StatType category)
    {
        Type = category;
        Strength = JewelryCommon.StatStrengthRange();
    }

    public void Apply(Player player, float add = 0, float multiplier = 0) => JewelStatEffect.StatsByType[Type].Apply(player, (Strength + add) * multiplier);
    public float GetEffectValue(Player player, float add = 0f) => JewelStatEffect.StatsByType[Type].GetEffectBonus(player, Strength + add);

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

        return stat.Description.WithFormatArgs(stat.GetEffectBonus(player, Strength).ToString("#0.##")).Value + stars;
    }
}
