namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class AbsolutionStat : JewelStatEffect
{
    public override StatType Type => StatType.Absolution;
    public override Color Color => new(135, 135, 135);

    public override StatExclusivity Exclusivity => StatExclusivity.Generic;

    public override void Apply(Player player, float strength) => player.GetModPlayer<AbsolutionPlayer>().bonus = GetEffectBonus(player, strength);
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.AbsolutionStrength * multiplier;

    private class AbsolutionPlayer : ModPlayer
    {
        public float bonus = 0;

        public override void ResetEffects() => bonus = 0;

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) => modifiers.DefenseEffectiveness *= 1 - (bonus * 0.01f);
    }
}
