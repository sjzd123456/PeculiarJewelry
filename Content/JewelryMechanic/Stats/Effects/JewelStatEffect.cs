using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal abstract class JewelStatEffect : ModType
{
    public static Dictionary<StatCategory, JewelStatEffect> StatByCategory = new();

    public abstract StatCategory Category { get; }

    protected sealed override void Register()
    {
        ModTypeLookup<JewelStatEffect>.Register(this);
        StatByCategory.Add(Category, this);
    }

    public abstract void Apply(Player player);
}
