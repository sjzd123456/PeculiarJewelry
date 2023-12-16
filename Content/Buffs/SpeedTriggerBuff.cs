using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;

namespace PeculiarJewelry.Content.Buffs;

internal class SpeedTriggerBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex) 
        => player.GetModPlayer<SpeedPlayer>().speed += player.GetModPlayer<StackableBuffTracker>().StackableStrength("Speed");

    class SpeedPlayer : ModPlayer
    {
        internal float speed = 0;

        public override void ResetEffects() => speed = 0;
        public override void PreUpdateMovement() => Player.moveSpeed += speed;
    }
}
