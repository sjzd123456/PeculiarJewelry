using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;

namespace PeculiarJewelry.Content.Buffs;

internal class DodgeTriggerBuff : ModBuff
{
    public override void SetStaticDefaults() => BuffSet.TriggerBuffs.Add(Type);

    public override void Update(Player player, ref int buffIndex) 
        => player.GetModPlayer<DodgePlayer>().chance += player.GetModPlayer<StackableBuffTracker>().StackableStrength("Dodge");

    class DodgePlayer : ModPlayer
    {
        internal float chance = 0;

        public override void ResetEffects() => chance = 0;

        public override bool FreeDodge(Player.HurtInfo info)
        {
            bool dodge = Main.rand.NextFloat() < chance / 100f;

            if (dodge)
            {
                Player.SetImmuneTimeForAllTypes(45);
                Player.immune = true;
                Player.immuneNoBlink = false;
            }

            return dodge;
        }
    }
}
