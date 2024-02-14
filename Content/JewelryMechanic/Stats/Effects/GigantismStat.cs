namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class GigantismStat : JewelStatEffect
{
    public override StatType Type => StatType.Gigantism;
    public override Color Color => Color.IndianRed;
    public override StatExclusivity Exclusivity => StatExclusivity.Melee;

    public override void Apply(Player player, float strength) => player.GetModPlayer<GigantismPlayer>().bonus += GetEffectBonus(player, strength) / 100f;
    protected override float InternalEffectBonus(float multiplier, Player player) => PeculiarJewelry.StatConfig.GigantismStrength * multiplier;

    class GigantismPlayer : ModPlayer
    {
        public float bonus = 0f;

        public override void ResetEffects() => bonus = 0f;
        public override void ModifyItemScale(Item item, ref float scale) => scale += bonus;
    }
}
