namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class AllureStat : JewelStatEffect
{
    public override StatType Type => StatType.Allure;
    public override Color Color => Color.Turquoise;

    public override StatExclusivity Exclusivity => StatExclusivity.Utility;

    public override void Apply(Player player, float strength) => player.fishingSkill += (int)GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => (int)(PeculiarJewelry.StatConfig.OrderStrength * multiplier);
}
