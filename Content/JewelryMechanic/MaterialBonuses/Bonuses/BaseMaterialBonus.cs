using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;
using System.Collections.Generic;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal abstract class BaseMaterialBonus : ModType
{
    public static readonly Dictionary<string, BaseMaterialBonus> BonusesByKey = new();

    public abstract string MaterialKey { get; }
    public virtual int GetMajorJewelCount => 1;

    protected sealed override void Register()
    {
        ModTypeLookup<BaseMaterialBonus>.Register(this);
        BonusesByKey.Add(MaterialKey, this);
    }

    public int CountMaterial(Player player) => player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

    public virtual bool AppliesToStat(Player player, StatType type) => false;

    public virtual float EffectBonus(Player player, StatType statType) => 1f;
    public virtual void StaticBonus(Player player, bool firstSet) { }
    public virtual void SingleJewelBonus(Player player, BasicJewelry jewel) { }
    public virtual void ResetSingleJewelBonus(Player player, BasicJewelry jewel) { }
    public virtual float TriggerCoefficientBonus() => 1;
}
