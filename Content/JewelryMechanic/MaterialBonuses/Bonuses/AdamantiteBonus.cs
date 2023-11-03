using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using Terraria;
using Terraria.WorldBuilding;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class AdamantiteBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Adamantite";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) => type == StatType.Exactitude || type == StatType.Exploitation;
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
            player.GetModPlayer<AdamantiteBonusPlayer>().threeSet = true;
    }

    // Needs 5-Set

    class AdamantiteBonusPlayer : ModPlayer
    {
        internal bool threeSet = false;

        private float _chance;
        private StatModifier _mods;
        private int _baseDamage;

        public override void ResetEffects() => threeSet = false;

        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!threeSet)
                return;

            _chance = (item.crit + 4) / 100f;
            _mods = modifiers.CritDamage;
            _baseDamage = item.damage;

            modifiers.ModifyHitInfo += ModifyCritStack;
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (!threeSet)
                return;

            _chance = proj.CritChance / 100f;
            _mods = modifiers.CritDamage;
            _baseDamage = proj.damage;

            modifiers.ModifyHitInfo += ModifyCritStack;
        }

        private void ModifyCritStack(ref NPC.HitInfo info)
        {
            if (info.Crit)
            {
                float critDamage = _mods.ApplyTo(_baseDamage);

                while (true)
                {
                    _chance /= 2;

                    if (Main.rand.NextFloat() < _chance)
                        info.Damage += (int)critDamage;
                    else
                        break;
                }
            }
        }
    }
}