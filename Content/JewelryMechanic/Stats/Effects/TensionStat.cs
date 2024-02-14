using Terraria.DataStructures;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class TensionStat : JewelStatEffect
{
    public override StatType Type => StatType.Tension;
    public override Color Color => Color.Green;

    public override StatExclusivity Exclusivity => StatExclusivity.Ranged;

    public override void Apply(Player player, float strength) => player.GetModPlayer<TensionPlayer>().bonus += GetEffectBonus(player, strength) / 100f;
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.TensionStrength * multiplier;

    class TensionPlayer : ModPlayer
    {
        public float bonus = 0;

        public override void ResetEffects() => bonus = 0f;
    }

    class TensionProjectile : GlobalProjectile
    {
        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (source is EntitySource_Parent { Entity: Player } parent)
            {
                float bonus = (parent.Entity as Player).GetModPlayer<TensionPlayer>().bonus;

                if (bonus <= 0)
                    return;

                projectile.velocity *= bonus;
            }
        }
    }
}
