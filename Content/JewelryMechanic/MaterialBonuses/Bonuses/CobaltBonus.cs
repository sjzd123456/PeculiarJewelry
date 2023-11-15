using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using Terraria.DataStructures;

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

        public override void ResetEffects() => threeSet = false;
    }

    private class CobaltBonusProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        private bool _shadow = false;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_ItemUse_WithAmmo itemUse && itemUse.Player.GetModPlayer<CobaltBonusPlayer>().threeSet)
            {
                if (!projectile.minion)
                    return;

                projectile.GetGlobalProjectile<CobaltBonusProjectile>()._shadow = true;
            }
        }

        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            return base.PreDraw(projectile, ref lightColor);
        }
    }
}
