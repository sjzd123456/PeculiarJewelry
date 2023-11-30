using Terraria.DataStructures;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class HellstoneBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Hellstone";

    public override void StaticBonus(Player player, bool firstSet)
    {
        if (CountMaterial(player) >= 5)
            player.GetModPlayer<HellstoneBonusPlayer>().fiveSet = true;
    }

    class HellstoneBonusPlayer : ModPlayer
    {
        internal bool fiveSet = false;

        public override void ResetEffects() => fiveSet = false;

        public override void ModifyItemScale(Item item, ref float scale)
        {
            if (fiveSet)
                scale *= 1.2f;
        }
    }

    class HellstoneBonusProjectile : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_ItemUse_WithAmmo ammo && ammo.Player.GetModPlayer<HellstoneBonusPlayer>().fiveSet)
            {
                projectile.position = projectile.Center;
                projectile.scale *= 2f;
                projectile.width *= 2;
                projectile.height *= 2;
                projectile.position -= projectile.Size / 2f;
            }
        }
    }
}
