using PeculiarJewelry.Content.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class ShroomiteBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Shroomite";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) 
        => type == StatType.Exactitude || type == StatType.Exploitation || type == StatType.Vigor || type == StatType.Renewal;
    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.25f;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1f;

    public override float EffectBonus(Player player, StatType type)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);
        bool defensive = type == StatType.Renewal || type == StatType.Vigor;

        if (count >= 1)
            return defensive ? bonus : 0.94f;
        return 1f;
    }

    public override void StaticBonus(Player player, bool firstSet)
    {
        int count = CountMaterial(player);

        if (count >= 3)
            player.GetModPlayer<ShroomiteBonusPlayer>().threeSet = true;

        if (count >= 5)
            player.GetModPlayer<ShroomiteBonusPlayer>().fiveSet = true;
    }

    // Needs 5-Set

    class ShroomiteBonusPlayer : ModPlayer
    {
        internal bool threeSet = false;
        internal bool fiveSet = false;

        public override void ResetEffects() => threeSet = fiveSet = false;

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!threeSet)
                return;

            int chance = Player.GetWeaponCrit(item);

            if (chance > 100)
            {
                int add = (chance - 100) / 100;
                modifiers.CritDamage += add;
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!threeSet)
                return;

            int chance = proj.CritChance;

            if (chance > 100)
            {
                int add = (chance - 100) / 100;
                modifiers.CritDamage += add;
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (fiveSet && hit.Crit && !proj.IsMinionOrSentryRelated && proj.whoAmI != Main.player[proj.owner].heldProj)
                DuplicateProjectile(proj, target);
        }

        private static void DuplicateProjectile(Projectile proj, NPC target)
        {
            var owner = Main.player[proj.owner];
            var magnitude = proj.velocity.Length();
            Vector2 vel = owner.DirectionTo(target.Center + (target.velocity * magnitude)) * magnitude;
            Projectile.NewProjectile(proj.GetSource_OnHit(target), owner.Center, vel, proj.type, proj.damage, proj.knockBack);
        }
    }
}
