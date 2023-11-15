using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;
using Terraria.GameContent;
using Terraria.UI.Chat;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class TitaniumBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Titanium";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) => type == StatType.Potency || type == StatType.Absolution;
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
        if (CountMaterial(player) >= 3)
            player.GetModPlayer<TitaniumBonusPlayer>().threeSet = true;
    }

    // Needs 5-Set

    private class TitaniumBonusPlayer : ModPlayer
    {
        internal bool threeSet = false;

        private int _lastNPCHit = -1;

        public override void ResetEffects() => threeSet = false;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (threeSet)
            {
                UpdateHitNPC(target);

                target.AddBuff(ModContent.BuffType<TitaniumDefenseDebuff>(), 2);
                target.GetGlobalNPC<TitaniumGlobalNPC>().debuffStacks++;
            }
        }

        private void UpdateHitNPC(NPC target)
        {
            if (_lastNPCHit != target.whoAmI && _lastNPCHit >= 0)
                Main.npc[_lastNPCHit].GetGlobalNPC<TitaniumGlobalNPC>().debuffStacks = 0;

            _lastNPCHit = target.whoAmI;
        }
    }

    private class TitaniumDefenseDebuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true; // Not for players but yeah
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (npc.GetGlobalNPC<TitaniumGlobalNPC>().debuffStacks <= 0)
            {
                npc.buffTime[buffIndex] = 0;
                buffIndex--;
            }
        }
    }

    private class TitaniumGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        internal int debuffStacks = 0;

        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            if (debuffStacks <= 0)
                return null;

            return GetTint(npc, drawColor);
        }

        private Color GetTint(NPC npc, Color drawColor)
        {
            const float Max = 0.6f;

            float factor = debuffStacks / (npc.defense * 0.5f);
            return Color.Lerp(drawColor, Color.Red, Math.Min(Max, factor * Max));
        }

        private int EffectiveStacks(NPC npc) => Math.Min(npc.defense / 2, debuffStacks);

        public override void ModifyHitByItem(NPC npc, Player p, Item i, ref NPC.HitModifiers modifiers) => modifiers.FlatBonusDamage += EffectiveStacks(npc);
        public override void ModifyHitByProjectile(NPC npc, Projectile p, ref NPC.HitModifiers modifiers) => modifiers.FlatBonusDamage += EffectiveStacks(npc);

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (debuffStacks > 0)
            {
                var val = debuffStacks.ToString();
                Vector2 origin = FontAssets.DeathText.Value.MeasureString(val) / 2f;
                ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.DeathText.Value, val, 
                    new Vector2(npc.Center.X, npc.position.Y - 22) - Main.screenPosition, GetTint(npc, drawColor), 0f, origin, new(0.75f));
            }
        }
    }
}
