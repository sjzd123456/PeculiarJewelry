
namespace PeculiarJewelry.Content.JewelryMechanic.Desecration.Definitions;

internal class DamageResistDesecration : DesecrationModifier
{
    public override float StrengthCap => 3f;
    public override float Profanity => 2f;

    public override void PreAI(NPC npc) => npc.GetGlobalNPC<ResistNPC>().damageResist = (int)(0.15f * strength);

    private class ResistNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public float damageResist = 0;

        public override void ResetEffects(NPC npc) => damageResist = 0;
        public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers) => modifiers.FinalDamage *= 1 - (damageResist / 100f);
    }
}
