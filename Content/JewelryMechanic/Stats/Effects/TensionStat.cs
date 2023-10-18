namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class TensionStat : JewelStatEffect
{
    public override StatType Type => StatType.Tension;
    public override Color Color => Color.Green;

    public override StatExclusivity Exclusivity => StatExclusivity.Ranged;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier, Player player) 
        => PeculiarJewelry.StatConfig.TensionStrength * multiplier * player.MaterialBonus("Mythril", Type);
}
