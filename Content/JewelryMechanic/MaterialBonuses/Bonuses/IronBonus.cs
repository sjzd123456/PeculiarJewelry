using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class IronBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Iron";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) => type == StatType.Permenance;
    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.15f;
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
            player.GetModPlayer<IronBonusPlayer>().threeSet = true;
    }

    // Needs 5-Set

    class IronBonusPlayer : ModPlayer
    {
        internal bool threeSet;

        public override void ResetEffects() => threeSet = false;

        public override void ModifyHurt(ref Player.HurtModifiers modifiers) => modifiers.ModifyHurtInfo += ModDamage;

        private void ModDamage(ref Player.HurtInfo info)
        {
            if (threeSet && info.Damage <= Player.statDefense * 2)
                info.Damage = (int)(info.Damage * 0.5f);
        }
    }
}
