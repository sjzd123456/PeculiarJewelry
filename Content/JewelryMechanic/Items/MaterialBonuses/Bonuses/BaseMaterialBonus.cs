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

    public abstract float EffectBonus(Player player);
    public virtual void StaticBonus(Player player) { }
}
