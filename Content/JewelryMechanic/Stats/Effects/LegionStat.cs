namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class LegionStat : JewelStatEffect
{
    public override StatType Type => StatType.Legion;
    public override Color Color => Color.CornflowerBlue;

    public override StatExclusivity Exclusivity => StatExclusivity.Summon;

    public override void Apply(Player player, float strength)
    {
    }

    public override float GetEffectValue(float multiplier, Player player) 
        => PeculiarJewelry.StatConfig.LegionStrength * multiplier * player.MaterialBonus("Cobalt", Type);
}
