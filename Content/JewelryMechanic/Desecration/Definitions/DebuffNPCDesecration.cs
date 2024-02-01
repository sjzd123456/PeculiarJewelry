
using Terraria.DataStructures;

namespace PeculiarJewelry.Content.JewelryMechanic.Desecration.Definitions;

internal class DebuffNPCDesecration : DesecrationModifier
{
    public override float StrengthCap => 5;
    public override float Profanity => 2;

    private class DebuffNPC : GlobalNPC
    {
        private static readonly int[] BuffIds = [BuffID.CursedInferno, BuffID.OnFire, BuffID.OnFire3, BuffID.Ichor, BuffID.ShadowFlame];

        public override void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo) => DebuffPlayer(target);

        public static void DebuffPlayer(Player target)
        {
            float str = Desecrations[nameof(DebuffNPCDesecration)].strength;

            if (str > 0 && Main.rand.NextFloat() > 0.15f * str)
                target.AddBuff(Main.rand.Next(BuffIds), (int)(30 * 60 + (10 * 60 * str)));
        }
    }

    private class DebuffProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        private bool _canDebuff = false;

        public override void OnSpawn(Projectile projectile, IEntitySource source) => _canDebuff = source is EntitySource_Parent { Entity: NPC } parent
            && (parent.Entity as NPC).damage > 0 && projectile.damage > 0;

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            if (_canDebuff)
                DebuffNPC.DebuffPlayer(target);
        }
    }
}
