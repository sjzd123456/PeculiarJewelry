using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class ShroomiteBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Shroomite";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) 
        => type == StatType.Exactitude || type == StatType.Exploitation || type == StatType.Vigor || type == StatType.Renewal;
    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.25f;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1f;

    public override float EffectBonus(Player player, StatType type)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);
        bool defensive = type == StatType.Renewal || type == StatType.Vigor;

        if (count >= 1)
            return defensive ? bonus : 0.94f;
        return 1f;
    }

    public override void StaticBonus(Player player, bool firstSet)
    {
        if (player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey) >= 3)
            player.GetModPlayer<ShroomiteBonusPlayer>().threeSet = true;
    }

    // Needs 5-Set

    class ShroomiteBonusPlayer : ModPlayer
    {
        internal bool threeSet = false;

        public override void ResetEffects() => threeSet = false;

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!threeSet)
                return;

            int chance = Player.GetWeaponCrit(item);

            if (chance > 100)
            {
                int add = (chance - 100) / 100;
                modifiers.CritDamage += add;
            }
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!threeSet)
                return;

            int chance = proj.CritChance;

            if (chance > 100)
            {
                int add = (chance - 100) / 100;
                modifiers.CritDamage += add;
            }
        }
    }
}
