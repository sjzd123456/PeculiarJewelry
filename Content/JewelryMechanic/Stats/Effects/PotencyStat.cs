namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class PotencyStat : JewelStatEffect
{
    public override StatType Type => StatType.Potency;
    public override Color Color => new(201, 201, 201);

    public override StatExclusivity Exclusivity => StatExclusivity.Generic;

    public override void Apply(Player player, float strength) => player.GetDamage(DamageClass.Generic) += GetEffectValue(strength, player) * 0.01f;
    public override float GetEffectValue(float multiplier, Player player) => PeculiarJewelry.StatConfig.PotencyStrength * multiplier;
}
