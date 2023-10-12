namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class ResurgenceStat : JewelStatEffect
{
    public override StatType Type => StatType.Resurgence;
    public override Color Color => Color.Lavender;

    public override StatExclusivity Exclusivity => StatExclusivity.Magic;

    public override void Apply(Player player, float strength) => player.manaRegen += (int)GetEffectValue(strength, player);
    public override float GetEffectValue(float multiplier, Player player) => PeculiarJewelry.StatConfig.ResurgenceStrength * multiplier;
}
