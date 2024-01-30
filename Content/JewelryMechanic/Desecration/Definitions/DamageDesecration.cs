
namespace PeculiarJewelry.Content.JewelryMechanic.Desecration.Definitions;

internal class DamageDesecration : DesecrationModifier
{
    public override float StrengthCap => 6f;

    public override void PostSetDefaults(NPC npc) => npc.damage = (int)(npc.damage * (1 + (0.33f * strength)));
}
