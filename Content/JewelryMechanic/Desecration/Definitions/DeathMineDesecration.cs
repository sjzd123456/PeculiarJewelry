using System;
using System.Composition;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace PeculiarJewelry.Content.JewelryMechanic.Desecration.Definitions;

internal class DeathMineDesecration : DesecrationModifier
{
    private const float ExplosionRadius = 120;

    public override float Profanity => 3f;
    public override float StrengthCap => 1f;

    public static void Explode(Entity entity, bool clientSideEffects)
    {
        if (Desecrations[nameof(DeathMineDesecration)].strength < 0)
            return;

        float increase = MathF.Pow(ExplosionRadius / 80f, 1.2f);

        if (Main.netMode != NetmodeID.Server)
            SoundEngine.PlaySound(SoundID.Item14, entity.Center);

        for (int i = 0; i < 25 * increase; ++i)
        {
            if (clientSideEffects)
            {
                Dust.NewDust(entity.position, entity.width, entity.height, DustID.Smoke, Main.rand.NextFloat(-6, 6) * increase, Main.rand.NextFloat(-6, 6) * increase);
                Dust.NewDust(entity.position, entity.width, entity.height, DustID.Torch, Main.rand.NextFloat(-6, 6) * increase, Main.rand.NextFloat(-6, 6) * increase);
            }
            else
                Projectile.NewProjectile(entity.GetSource_Death(), entity.Center, Main.rand.NextVector2Circular(6, 6) * increase, ModContent.ProjectileType<Smoke>(), 0, 0);
        }

        if (Main.netMode == NetmodeID.SinglePlayer)
            HurtPlayer(entity, Main.myPlayer, Main.LocalPlayer);
        else
        {
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player player = Main.player[i];

                if (!player.active || player.dead)
                    continue;

                HurtPlayer(entity, i, player);
            }
        }
    }

    private static void HurtPlayer(Entity entity, int playerWhoAmI, Player player)
    {
        PlayerDeathReason reason = entity is NPC ? PlayerDeathReason.ByNPC(entity.whoAmI) : PlayerDeathReason.ByProjectile(playerWhoAmI, entity.whoAmI);

        if (player.DistanceSQ(entity.Center) < ExplosionRadius * ExplosionRadius)
            player.Hurt(reason, 100, 0, false, false, -1, false, 20);
    }

    private class ExplodingNPC : GlobalNPC
    {
        public override void OnKill(NPC npc) => Explode(npc, false);

        public override void HitEffect(NPC npc, NPC.HitInfo hit)
        {
            if (npc.life <= 0)
                Explode(npc, true);
        }
    }

    private class ExplodingProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        private bool _shouldExplode = false;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            _shouldExplode = false;

            if (source is EntitySource_Parent { Entity: NPC } parent && (parent.Entity as NPC).CanBeChasedBy() && projectile.damage > 0 && projectile.hostile)
                _shouldExplode = true;
        }

        public override void OnKill(Projectile projectile, int timeLeft)
        {
            if (!_shouldExplode)
                return;

            Explode(projectile, true);
            Explode(projectile, false);
        }
    }

    class Smoke : ModProjectile
    {
        public override string Texture => "PeculiarJewelry/Assets/Textures/Smoke";

        private Color drawCol = Color.Green;
        private int _maxTimeLeft = 0;

        public override void SetDefaults()
        {
            Projectile.hostile = false;
            Projectile.friendly = false;
            Projectile.width = 30;
            Projectile.height = 38;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.scale = Main.rand.NextFloat(0.9f, 1.1f);
            Projectile.timeLeft = (int)(Main.rand.Next(30, 60) * Projectile.scale);
            Projectile.frame = Main.rand.Next(4);
            Projectile.damage = 0;

            _maxTimeLeft = Projectile.timeLeft;
        }

        public override void AI()
        {
            Projectile.rotation += 0.05f * Projectile.velocity.Length();
            Projectile.velocity *= 0.92f;

            float section = _maxTimeLeft / 5f;
            float time = (_maxTimeLeft - Projectile.timeLeft) % section / section;
            int currentSection = (int)((_maxTimeLeft - Projectile.timeLeft) / section);

            if (currentSection == 0)
                drawCol = Color.Lerp(Color.Yellow, Color.Orange, time);
            else if (currentSection == 1)
                drawCol = Color.Lerp(Color.Orange, Color.Red, time);
            else if (currentSection == 2)
                drawCol = Color.Lerp(Color.Red, new Color(100, 100, 100), time);
            else if (currentSection == 3)
                drawCol = Color.Lerp(new Color(100, 100, 100), new Color(30, 30, 30), time);
            else if (currentSection == 4)
                drawCol = Color.Lerp(new Color(30, 30, 30), Color.Black * 0, time);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            var frame = new Rectangle(0, Projectile.frame * 42, 38, 40);
            Main.spriteBatch.Draw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, frame, drawCol * (Projectile.scale - 0.8f), 
                Projectile.rotation, new(19, 20), Projectile.scale, SpriteEffects.None, 1f);
            return false;
        }
    }
}