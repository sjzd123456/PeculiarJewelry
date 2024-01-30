using System;
using System.Linq;

namespace PeculiarJewelry.Content.JewelryMechanic.Desecration.Definitions;

internal class TrapDoTBuffDesecration : DesecrationModifier
{
    public override float StrengthCap => 2f;

    private class TrapDoTPlayer : ModPlayer
    {
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            float str = Desecrations[nameof(TrapDoTBuffDesecration)].strength;

            if (str <= 0)
                return;

            if (modifiers.DamageSource.SourceOtherIndex == 3) // Spikes
                modifiers.FinalDamage += str * 1.5f;

            int[] trapProjectiles = [ProjectileID.PoisonDartTrap, ProjectileID.VenomDartTrap, ProjectileID.Boulder, ProjectileID.LifeCrystalBoulder, 
                ProjectileID.RollingCactus];

            if (trapProjectiles.Contains(modifiers.DamageSource.SourceProjectileType))
                modifiers.FinalDamage += str * 1.5f;
        }

        public override void UpdateBadLifeRegen()
        {
            if (Desecrations[nameof(TrapDoTBuffDesecration)].strength <= 0)
                return;

            if (Player.lifeRegen < 0)
                Player.lifeRegen += (int)(Player.lifeRegen * 1.5f);
        }
    }
}
