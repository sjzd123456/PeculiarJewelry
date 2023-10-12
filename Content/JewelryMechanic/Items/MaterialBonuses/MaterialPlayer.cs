using PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses;

internal class MaterialPlayer : ModPlayer
{
    internal readonly struct EquipLayerInfo
    {
        public readonly Color Color;
        public readonly Texture2D Texture;

        public EquipLayerInfo(Color color, Texture2D texture)
        {
            Color = color;
            Texture = texture;
        }
    }

    internal readonly Dictionary<EquipType, EquipLayerInfo?> jewelryEquips = new();

    private readonly Dictionary<string, int> _materialsWornCount = new()
    {
        { "Copper", 0 },
        { "Tin", 0 },
        { "Iron", 0 },
        { "Lead", 0 },
        { "Silver", 0 },
        { "Tungsten", 0 },
        { "Gold", 0 },
        { "Platinum", 0 },
    };

    public override void ResetEffects()
    {
        foreach (var item in _materialsWornCount.Keys)
            _materialsWornCount[item] = 0;

        foreach (var item in jewelryEquips.Keys)
            jewelryEquips[item] = null;
    }

    public override void PostUpdateEquips()
    {
        
    }

    public void AddMaterial(string name) => _materialsWornCount[name]++;
    internal int MaterialCount(string materialKey) => _materialsWornCount[materialKey];

    internal void SetEquip(EquipType type, EquipLayerInfo info)
    {
        if (!jewelryEquips.ContainsKey(type))
            jewelryEquips.Add(type, info);
        else
            jewelryEquips[type] = info;
    }

    internal bool HasEquip(EquipType type, out EquipLayerInfo info)
    {
        info = default;

        if (jewelryEquips.ContainsKey(type) && jewelryEquips[type] is not null)
        {
            info = jewelryEquips[type].Value;
            return true;
        }
        return false;
    }

    internal void StaticMaterialEffects()
    {
        foreach (var mat in _materialsWornCount.Where(x => x.Value > 0))
            BaseMaterialBonus.BonusesByKey[mat.Key].StaticBonus(Player);
    }
}
