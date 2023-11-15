using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class CobaltBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Cobalt";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) => type == StatType.Abundance || type == StatType.Legion || type == StatType.Order;
    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.15f;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1f;

    public override float EffectBonus(Player player, StatType statType)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 1)
            return bonus;
        return 1f;
    }

    public override void StaticBonus(Player player, bool firstSet)
    {
        if (CountMaterial(player) >= 3)
            player.GetModPlayer<CobaltBonusPlayer>().threeSet = true;
    }

    // Needs 3-Set, 5-Set

    private class CobaltBonusPlayer : ModPlayer
    {
        internal bool threeSet = false;
        internal bool oldThree = false;

        public override void ResetEffects()
        {
            oldThree = threeSet;
            threeSet = false;
        }
    }

    private class CobaltBonusProjectile : GlobalProjectile
    {
        private static readonly int[] ExceptionIDs = new int[] { ProjectileID.StardustDragon1, ProjectileID.StardustDragon2, ProjectileID.StardustDragon3, 
            ProjectileID.StardustDragon4, ProjectileID.StormTigerTier1, ProjectileID.StormTigerTier2, ProjectileID.StormTigerTier3, ProjectileID.StormTigerGem };

        public override bool InstancePerEntity => true;

        private bool _shadow = false;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            bool tiger = source is EntitySource_Misc { Context: "StormTigerTierSwap" } && Main.player[projectile.owner].GetModPlayer<CobaltBonusPlayer>().oldThree;
            bool normal = source is EntitySource_ItemUse_WithAmmo itemUse && itemUse.Player.GetModPlayer<CobaltBonusPlayer>().threeSet;

            if (tiger || normal)
            {
                if (!projectile.minion)
                    return;

                if (projectile.GetGlobalProjectile<CobaltBonusProjectile>()._shadow)
                    return;

                if (ExceptionIDs.Contains(projectile.type))
                {
                    projectile.GetGlobalProjectile<CobaltBonusProjectile>()._shadow = true;
                    projectile.damage += projectile.damage / 2;
                    return;
                }

                int newProj = Projectile.NewProjectile(new EntitySource_Parent(projectile, "ShadowClone"), projectile.Center, projectile.velocity * -0.5f, 
                    projectile.type, projectile.damage / 2, projectile.knockBack, projectile.owner);
                Main.projectile[newProj].GetGlobalProjectile<CobaltBonusProjectile>()._shadow = true;
                Main.projectile[newProj].minionSlots = 0;
                Main.projectile[newProj].Opacity = 0.5f;
            }
        }

        public override bool PreAI(Projectile projectile)
        {
            if (projectile.TryGetOwner(out Player owner) && !owner.GetModPlayer<CobaltBonusPlayer>().threeSet)
            {
                projectile.Kill();
                return false;
            }

            return true;
        }

        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (projectile.GetGlobalProjectile<CobaltBonusProjectile>()._shadow)
                Main.instance.PrepareDrawnEntityDrawing(projectile, GameShaders.Armor.GetShaderIdFromItemId(ItemID.TwilightDye), null);

            return true;
        }
    }
}
