using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;

namespace PeculiarJewelry.Content.Buffs;

internal class DodgeTriggerBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex) 
        => player.GetModPlayer<DodgePlayer>().chance += player.GetModPlayer<StackableBuffTracker>().StackableStrength("Dodge");

    class DodgePlayer : ModPlayer
    {
        internal float chance = 0;

        public override void ResetEffects() => chance = 0;

        public override bool FreeDodge(Player.HurtInfo info) => Main.rand.NextFloat() < chance;
    }
}
