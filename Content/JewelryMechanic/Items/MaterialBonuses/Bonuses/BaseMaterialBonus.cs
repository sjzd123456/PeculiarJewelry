using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System.Collections.Generic;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;

internal abstract class BaseMaterialBonus : ModType
{
    public static readonly Dictionary<string, BaseMaterialBonus> BonusesByKey = new();

    public abstract string MaterialKey { get; }

    protected sealed override void Register()
    {
        ModTypeLookup<BaseMaterialBonus>.Register(this);
        BonusesByKey.Add(MaterialKey, this);
    }

    public virtual float EffectBonus(Player player, StatType statType) => 1f;
    public virtual void StaticBonus(Player player) { }
    public virtual void SingleJewelBonus(Player player, BasicJewelry jewel) { }
    public virtual void ResetSingleJewelBonus(Player player, BasicJewelry jewel) { }
}
