namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers.TriggerEffects;

internal class KBAndClearTrigger : TriggerEffect
{
    public override TriggerType Type => TriggerType.InstantOther;

    protected override void InternalInstantEffect(TriggerContext context, Player player, float coefficient, JewelTier tier)
    {
        float radius = TotalTriggerPower(player, coefficient) * 16 * 10;

        for (int i = 0; i < Main.maxNPCs; ++i)
        {
            NPC npc = Main.npc[i];

            if (npc.CanBeChasedBy() && npc.DistanceSQ(player.Center) < radius * radius)
                npc.velocity = player.DirectionTo(npc.Center) * MathHelper.Lerp(npc.knockBackResist, 1f, 0.5f) * 8;
        }

        for (int i = 0; i < Main.maxProjectiles; ++i)
        {
            Projectile projectile = Main.projectile[i];

            if (projectile.active && projectile.hostile && projectile.DistanceSQ(player.Center) < radius * radius)
                projectile.Kill();
        }
    }

    public override float TriggerPower() => 50;
}
