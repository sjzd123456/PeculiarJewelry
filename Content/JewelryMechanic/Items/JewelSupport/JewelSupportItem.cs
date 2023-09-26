using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelSupport;

public abstract class JewelSupportItem : ModItem
{
    public virtual bool CanBePlacedInSupportSlot(JewelInfo info) => true;
    public virtual float ModifyJewelCutChance(JewelInfo info, float baseChance) => baseChance;

    public virtual bool HardOverrideJewelCutChance(JewelInfo info, out float chance)
    {
        chance = 0;
        return false;
    }
}
