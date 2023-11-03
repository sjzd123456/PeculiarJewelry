using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class OrichalcumBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Orichalcum";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) => type == StatType.Arcane || type == StatType.Resurgence || type == StatType.Willpower;
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
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 3)
            player.GetModPlayer<OrichalcumBonusPlayer>().threeSet = true;
    }

    // Needs 5-Set

    class OrichalcumBonusPlayer : ModPlayer
    {
        internal bool threeSet = false;
        internal int projCount = 0;

        public override void ResetEffects() => threeSet = false;
    }

    class OrichalcumBonusProjectile : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (projectile.damage < 0 || !projectile.DamageType.CountsAsClass(DamageClass.Magic))
                return;

            if (source is EntitySource_ItemUse_WithAmmo ammo)
            {
                var player = ammo.Player;
                var plr = player.GetModPlayer<OrichalcumBonusPlayer>();

                if (plr.threeSet)
                {
                    if (plr.projCount == 2)
                    {
                        if (!player.CheckMana(ammo.Item.mana, true))
                            return;

                        plr.projCount = 0;

                        projectile.position = projectile.Center;
                        projectile.scale *= 1.5f;
                        projectile.Size *= 1.5f;
                        projectile.position -= projectile.Size / 2;
                        projectile.damage = (int)(projectile.damage * 2f);
                    }
                    else
                        plr.projCount++;
                }
            }
        }
    }
}
