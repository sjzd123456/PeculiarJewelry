using Terraria.GameContent.Drawing;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class MeteoriteBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Meteorite";

    public override void StaticBonus(Player player, bool firstSet)
    {
        if (CountMaterial(player) >= 5)
        {
            player.GetDamage(DamageClass.Generic) += player.velocity.Length() / 100f;
            player.GetModPlayer<MeteoriteBonusPlayer>().fiveSet = true;

            if (player.velocity.LengthSquared() > 12 * 12)
            {
                if (Main.rand.NextBool(6))
                ParticleOrchestrator.RequestParticleSpawn(false, ParticleOrchestraType.FlameWaders, new ParticleOrchestraSettings
                {
                    PositionInWorld = new Vector2(player.position.X + Main.rand.NextFloat(player.width), player.position.Y + Main.rand.NextFloat(player.height)),
                });

                if (Main.rand.NextBool(3))
                    Dust.NewDust(player.position, player.width, player.height, DustID.Torch);
            }
        }
    }

    internal class MeteoriteBonusPlayer : ModPlayer
    {
        internal bool fiveSet = false;

        public override void ResetEffects() => fiveSet = false;

        public override void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
        {
            if (fiveSet && Player.velocity.LengthSquared() > 12 * 12)
                npc.SimpleStrikeNPC(hurtInfo.Damage * 5, -hurtInfo.HitDirection, true, 5, null, true, 0);
        }
    }
}
