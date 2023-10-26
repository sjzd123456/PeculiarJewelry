﻿using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses;
using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;
using System.Collections.Generic;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

public abstract class JewelStatEffect : ModType
{
    public static readonly Dictionary<StatType, JewelStatEffect> StatsByType = new();

    public abstract StatType Type { get; }
    public abstract Color Color { get; }

    public virtual StatExclusivity Exclusivity => StatExclusivity.None;

    public LocalizedText DisplayName { get; protected set; }
    public LocalizedText Description { get; protected set; }

    protected sealed override void Register()
    {
        ModTypeLookup<JewelStatEffect>.Register(this);
        StatsByType.Add(Type, this);

        DisplayName = Language.GetText("Mods.PeculiarJewelry.Jewelry.StatTypes." + Type + ".DisplayName");
        Description = Language.GetText("Mods.PeculiarJewelry.Jewelry.StatTypes." + Type + ".Description");
    }

    public abstract void Apply(Player player, float strength);

    public float GetEffectBonus(Player player, float strength)
    {
        float value = InternalEffectBonus(strength, player);

        foreach (var item in BaseMaterialBonus.BonusesByKey.Keys)
        {
            if (player.GetModPlayer<MaterialPlayer>().MaterialCount(item) > 0 && BaseMaterialBonus.BonusesByKey[item].AppliesToStat(player, Type))
            {

            }
        }

        return value;
    }

    protected abstract float InternalEffectBonus(float multiplier, Player player);
}
