
namespace PeculiarJewelry.Content.JewelryMechanic.Desecration.Definitions;

internal class HealthBoostDesecration : DesecrationModifier
{
    public override float StrengthCap => 6f;

    public override void PostSetDefaults(NPC npc) => npc.lifeMax = (int)(npc.lifeMax * (1 + (0.5f * strength)));
}
