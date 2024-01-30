
namespace PeculiarJewelry.Content.JewelryMechanic.Desecration.Definitions;

/// <summary>
/// This is a reference and attack to Master Mode. Boring!
/// </summary>
internal class MMBoostDesecration : DesecrationModifier
{
    public override float StrengthCap => -1;

    public override void PostSetDefaults(NPC npc)
    {
        npc.damage = (int)(npc.damage * (1 + (0.1f * strength)));
        npc.lifeMax = (int)(npc.lifeMax * (1 + (0.1f * strength)));
        npc.defense += (int)(2 * strength);
    }

    public override void PreAI(NPC npc) => npc.GetGlobalNPC<NPCBehaviourBoostGlobal>().extraAISpeed += 0.05f * strength;
}
