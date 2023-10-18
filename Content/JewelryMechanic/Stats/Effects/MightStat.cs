namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class MightStat : JewelStatEffect
{
    public override StatType Type => StatType.Might;
    public override Color Color => Color.Crimson;

    public override StatExclusivity Exclusivity => StatExclusivity.Melee;

    public override void Apply(Player player, float strength) => player.GetDamage(DamageClass.Melee) += GetEffectValue(strength, player);
    public override float GetEffectValue(float multiplier, Player player)
        => PeculiarJewelry.StatConfig.MightStrength * multiplier * player.MaterialBonuses(Type, "Demonite", "Crimtane", "Palladium");
}
