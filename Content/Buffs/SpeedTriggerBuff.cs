using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;

namespace PeculiarJewelry.Content.Buffs;

internal class SpeedTriggerBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex) 
        => player.GetModPlayer<SpeedPlayer>().speed += player.GetModPlayer<StackableBuffTracker>().StackableStrength("Speed") / 70f;

    class SpeedPlayer : ModPlayer
    {
        internal float speed = 0;

        public override void ResetEffects() => speed = 0;
        
        public override void PostUpdateRunSpeeds()
        {
            Player.runAcceleration += speed;
            Player.maxRunSpeed += speed;

            if (Player.runAcceleration > Player.maxRunSpeed / 8f)
                Player.runAcceleration = Player.maxRunSpeed / 8f;
        }
    }
}
