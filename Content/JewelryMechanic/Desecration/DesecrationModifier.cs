using System.Collections.Generic;

namespace PeculiarJewelry.Content.JewelryMechanic.Desecration;

internal abstract class DesecrationModifier : ModType
{
    public static Dictionary<string, DesecrationModifier> Desecrations = [];

    public virtual float BaseModifier => 1f;
    public virtual float StrengthCap => -1;

    public string DesecrationName => Language.GetTextValue("Mods.PeculiarJewelry.Desecrations." + GetType().Name + ".Name");

    public float strength = 0;

    protected override void Register()
    {
        ModTypeLookup<DesecrationModifier>.Register(this);
        Desecrations.Add(GetType().Name, this);
    }

    public virtual void PostSetDefaults(NPC npc) { }
    public virtual void PreAI(NPC npc) { }
}
