namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class ResurgenceStat : JewelStatEffect
{
    public override StatType Type => StatType.Resurgence;
    public override Color Color => Color.LavenderBlush;

    public override StatExclusivity Exclusivity => StatExclusivity.Magic;

    public override void Apply(Player player, float strength, Item item) => player.manaRegen += (int)GetEffectValue(strength);
    public override float GetEffectValue(float multiplier) => PeculiarJewelry.StatConfig.ResurgenceStrength * multiplier;
}
