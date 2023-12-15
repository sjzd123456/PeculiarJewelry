using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;

namespace PeculiarJewelry.Content.Buffs;

internal class RegenTriggerBuff : ModBuff
{
    public override void Update(Player player, ref int buffIndex) 
        => player.GetModPlayer<RegenPlayer>().regen += player.GetModPlayer<StackableBuffTracker>().StackableStrength("Regen");

    class RegenPlayer : ModPlayer
    {
        internal float regen = 0;
        
        private float _oldRegen = 0;

        public override void ResetEffects()
        {
            _oldRegen = regen;
            regen = 0;
        }

        public override void UpdateLifeRegen() => Player.lifeRegen += (int)_oldRegen;
    }
}
