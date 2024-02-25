namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

/// <summary>
/// All damage. MP safe.
/// </summary>
internal class PotencyStat : JewelStatEffect
{
    public override StatType Type => StatType.Potency;
    public override Color Color => new(201, 201, 201);

    public override StatExclusivity Exclusivity => StatExclusivity.Generic;

    public override void Apply(Player player, float strength) => player.GetDamage(DamageClass.Generic) += GetEffectBonus(player, strength) / 100f;
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.PotencyStrength * multiplier * 0.4f;
}
