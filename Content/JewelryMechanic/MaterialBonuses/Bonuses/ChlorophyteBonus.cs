using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using Steamworks;
using System;
using Terraria;
using Terraria.GameContent;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class ChlorophyteBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Chlorophyte";

    float _bonus = 1f;
    Item _acc = null;

    public override bool AppliesToStat(Player player, StatType type) => true;

    public override void SingleJewelBonus(Player player, BasicJewelry jewel)
    {
        _bonus = 1.15f;
        _acc = jewel.Item;
    }

    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel)
    {
        _bonus = 1f;
        _acc = null;
    }

    public override float EffectBonus(Player player, StatType type)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);
        bool defensive = type == StatType.Permenance || type == StatType.Tenacity || type == StatType.Vigor || type == StatType.Renewal;

        if (count >= 1)
            return defensive ? _bonus : 0.95f;
        return 1f;
    }

    public override void StaticBonus(Player player, bool firstSet)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 3)
        {
            player.GetModPlayer<ChlorophyteBonusPlayer>().threeSet = true;
            var source = player.GetSource_Accessory(_acc);

            if (player.ownedProjectileCounts[ModContent.ProjectileType<TrapperMinion>()] < 3)
                Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero, ModContent.ProjectileType<TrapperMinion>(), 30, 1f, player.whoAmI);
        }
    }

    // Needs 5-Set

    private class ChlorophyteBonusPlayer : ModPlayer
    {
        internal bool threeSet = false;
        internal int lastNumMinions = 0;

        public override void ResetEffects()
        {
            lastNumMinions = Player.numMinions;
            threeSet = false;
        }
    }

    class TrapperMinion : ModProjectile
    {
        const int MaxRange = 100;

        private Player Owner => Main.player[Projectile.owner];

        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(26);
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.minion = true;

            AIType = -1;
        }

        public override void AI()
        {
            Projectile.timeLeft = 2;
            Projectile.ai[0]++;

            if (Owner.GetModPlayer<MaterialPlayer>().MaterialCount("Chlorophyte") < 3)
            {
                Projectile.Kill();
                return;
            }

            bool outOfRange = Projectile.DistanceSQ(Owner.Center) > MaxRange * MaxRange;
            if (outOfRange)
            {
                var dist = SpeedUpDistance();
                Projectile.velocity += Projectile.DirectionTo(Owner.Center) * dist * 2f;

                dist = Math.Max(10, dist);
                if (Projectile.velocity.LengthSquared() > dist * dist)
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * dist;
            }

            float min = Owner.GetModPlayer<ChlorophyteBonusPlayer>().lastNumMinions;
            float offset = MathHelper.Lerp(-min / 2f, min / 2f, Projectile.minionPos / (min - 1)) * (MathHelper.TwoPi * 0.9f);
            float sin = 0.6f + MathF.Sin(Projectile.ai[0] * 0.02f) * 0.1f;
            Vector2 target = Owner.Center - new Vector2(0, MaxRange * sin).RotatedBy(offset + Projectile.ai[0] * 0.005f);

            NPC nearest = null;
            nearest = ScanNPCs(nearest, target);

            float magnitude;

            if (nearest is not null)
            {
                target = nearest.Center;
                magnitude = SpeedUpDistance() * 0.1f + 4;
            }
            else
            {
                magnitude = Projectile.Distance(target) * 0.2f;
                Projectile.velocity *= 0.98f;
            }

            Vector2 dir = Projectile.DirectionTo(target) * magnitude;

            if (!outOfRange || Owner.DistanceSQ(Projectile.Center) > Owner.DistanceSQ(Projectile.Center - dir))
            {
                Projectile.velocity += dir;

                if (!outOfRange && Projectile.velocity.LengthSquared() > 10 * 10)
                    Projectile.velocity = Vector2.Normalize(Projectile.velocity) * 10;
            }
        }

        private NPC ScanNPCs(NPC nearest, Vector2 origin)
        {
            for (int i = 0; i < Main.maxNPCs; ++i)
            {
                NPC npc = Main.npc[i];
                float dist = npc.DistanceSQ(Owner.Center);

                if (npc.CanBeChasedBy() && dist <= MaxRange * MaxRange && (nearest is null || nearest.DistanceSQ(origin) > dist))
                    nearest = npc;
            }

            return nearest;
        }

        private float SpeedUpDistance() => ((Projectile.Distance(Owner.Center) - MaxRange) / 20f) + 1f;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => Owner.Heal((int)(damageDone * 0.2f));

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 origin = Owner.Center;
            float dist = Projectile.Distance(origin);
            int length = 16;
            Vector2 dir = Projectile.DirectionTo(origin);
            float rot = dir.ToRotation() - MathHelper.PiOver2;

            for (int i = 6; i < dist; i += length)
            {
                var pos = origin - (dir * i) - Main.screenPosition;
                int frm = i % 3;
                var src = new Rectangle(6, 32 + (18 * frm), 14, 16);
                var col = Lighting.GetColor((pos + Main.screenPosition).ToTileCoordinates());
                Main.spriteBatch.Draw(TextureAssets.Projectile[Type].Value, pos, src, col, rot, new(7, 8), 1f, SpriteEffects.None, 0);
            }

            var drawPos = Projectile.Center - Main.screenPosition;
            Main.spriteBatch.Draw(TextureAssets.Projectile[Type].Value, drawPos, new(0, 0, 26, 30), lightColor, rot, new(13, 15), 1f, SpriteEffects.None, 0);
            return false;
        }
    }
}
