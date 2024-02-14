namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class ToleranceStat : JewelStatEffect
{
    public override StatType Type => StatType.Tolerance;
    public override Color Color => new(217, 200, 174);

    public override StatExclusivity Exclusivity => StatExclusivity.Utility;

    public override void Apply(Player player, float strength)
    {
        player.GetModPlayer<TolerancePlayer>().bonus += GetEffectBonus(player, strength);
        player.breathMax += (int)GetEffectBonus(player, strength);
    }

    protected override float InternalEffectBonus(float multiplier, Player player) => (int)(PeculiarJewelry.StatConfig.ToleranceStrength * multiplier);

    public override void Load()
    {
        On_Player.AddBuff += HijackAddBuff;
    }

    private void HijackAddBuff(On_Player.orig_AddBuff orig, Player self, int type, int timeToAdd, bool quiet, bool foodHack)
    {
        if (!Main.debuff[type])
            timeToAdd = (int)(timeToAdd * (1 + self.GetModPlayer<TolerancePlayer>().bonus));
        else
            timeToAdd = (int)(timeToAdd * (1 - self.GetModPlayer<TolerancePlayer>().bonus));

        orig(self, type, timeToAdd, quiet, foodHack);

    }

    class TolerancePlayer : ModPlayer
    {
        internal float bonus = 0;

        public override void ResetEffects() => bonus = 0;
    }
}
