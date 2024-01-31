
namespace PeculiarJewelry.Content.JewelryMechanic.Desecration.Definitions;

internal class BossHealDesecration : DesecrationModifier
{
    public override float StrengthCap => 1;
    public override float Profanity => 3;

    private class HealNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(NPC entity, bool lateInstantiation) => entity.boss;

        private byte _healSlot = 2;

        public override void SetDefaults(NPC entity) => _healSlot = 2;

        public override bool PreAI(NPC npc)
        {
            if (Desecrations[nameof(BossHealDesecration)].strength <= 0 || _healSlot == 0)
                return true;

            if (_healSlot == 2 && npc.life < npc.lifeMax * 0.67f)
            {
                Heal(npc);
                _healSlot = 1;
            }
            else if (_healSlot == 1 && npc.life < npc.lifeMax * 0.33f)
            {
                Heal(npc);
                _healSlot = 0;
            }

            return true;
        }

        private static void Heal(NPC npc)
        {
            int amt = (int)(npc.lifeMax / 4f);
            npc.HealEffect(amt, true);
            npc.life += amt;
        }
    }
}
