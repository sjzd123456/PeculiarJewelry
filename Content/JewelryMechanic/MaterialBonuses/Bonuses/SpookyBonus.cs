using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class SpookyBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Spooky";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) 
        => type == StatType.Exploitation || type == StatType.Exactitude || type == StatType.Renewal || type == StatType.Vigor;
    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.25f;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1f;

    public override float EffectBonus(Player player, StatType type)
    {
        bool movement = type == StatType.Exploitation || type == StatType.Exactitude;

        if (CountMaterial(player) >= 1)
            return movement ? bonus : 0.94f;
        return 1f;
    }

    public override void StaticBonus(Player player, bool firstSet)
    {
        if (CountMaterial(player) >= 3)
            player.GetModPlayer<SpookyBonusPlayer>().threeSet = true;
    }

    // Needs 5-Set

    class SpookyBonusPlayer : ModPlayer 
    {
        internal bool threeSet = false;

        public override void ResetEffects() => threeSet = true;

        public override void UpdateBadLifeRegen()
        {
            if (threeSet)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;

                Player.lifeRegenCount += 50 + Player.lifeRegen;
            }
        }
    }
}
