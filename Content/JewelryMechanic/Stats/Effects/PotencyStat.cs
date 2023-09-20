namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class PotencyStat : JewelStatEffect
{
    public override StatType Type => StatType.Potency;
    public override Color Color => new(201, 201, 201);

    public override StatExclusivity Exclusivity => StatExclusivity.Generic;

    public override void Apply(Player player, float strength, Item item) => player.GetDamage(DamageClass.Generic) += GetEffectValue(strength) * 0.01f;
    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.PotencyStrength * multiplier;
}
