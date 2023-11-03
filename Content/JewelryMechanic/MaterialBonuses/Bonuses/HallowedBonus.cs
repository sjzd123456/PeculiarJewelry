using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using Terraria.DataStructures;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class HallowedBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Hallowed";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) => true;
    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.05f;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1f;

    public override float EffectBonus(Player player, StatType statType)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 1)
            return bonus;
        return 1f;
    }

    public override void StaticBonus(Player player, bool firstSet)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 3)
            player.GetModPlayer<HallowedBonusPlayer>().threeSet = true;
    }

    // Needs 5-Set

    class HallowedBonusPlayer : ModPlayer
    {
        internal bool IsImmune => threeSet && !Main.CurrentFrameFlags.AnyActiveBossNPC && Main.invasionType == InvasionID.None;
        internal bool threeSet = false;

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (IsImmune)
            {
                playSound = genGore = false;
                Player.statLife = 1;
                return false;
            }

            return true;
        }
    }
}
