namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;

internal class TriggerCooldownBuff : ModBuff
{
    public override string Name => Identifier;
    public override string Texture => "PeculiarJewelry/Content/JewelryMechanic/Stats/Triggers/Textures/" + Name;

    readonly string Identifier;
    readonly LocalizedText ShowName;
    readonly LocalizedText Tooltip;

    public TriggerCooldownBuff(string identifier, LocalizedText name, LocalizedText tooltip)
    {
        Identifier = identifier;
        ShowName = name;
        Tooltip = tooltip;
    }

    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        buffName = ShowName.Value;
        tip = Tooltip.Value;
        rare = ItemRarityID.Red;
    }
}
