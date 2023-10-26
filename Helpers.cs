global using Terraria;
global using Terraria.ID;
global using Microsoft.Xna.Framework;
global using Microsoft.Xna.Framework.Graphics;
global using Terraria.Localization;
global using Terraria.ModLoader;

using PeculiarJewelry.Content.JewelryMechanic.Stats;
using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

namespace PeculiarJewelry;

public static class Extensions
{
    public static float MaterialBonus(this Player player, string material, StatType type) => BaseMaterialBonus.BonusesByKey[material].EffectBonus(player, type);

    public static float MaterialBonuses(this Player player, StatType type, params string[] materials)
    {
        float value = 1f;

        foreach (var mat in materials)
            value *= BaseMaterialBonus.BonusesByKey[mat].EffectBonus(player, type);

        return value;
    }

    public static void SingleBonus(this Player player, string mat, BasicJewelry jewel) => BaseMaterialBonus.BonusesByKey[mat].SingleJewelBonus(player, jewel);
    public static void UndoSingle(this Player player, string mat, BasicJewelry jewel) => BaseMaterialBonus.BonusesByKey[mat].ResetSingleJewelBonus(player, jewel);
}