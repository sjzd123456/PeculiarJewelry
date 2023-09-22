namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelSupport;

public abstract class JewelSupportItem : ModItem
{
    public virtual float ModifyJewelCutChance(float baseChance) => baseChance;

    public virtual bool HardOverrideJewelCutChance(out float chance)
    {
        chance = 0;
        return false;
    }
}
