using PeculiarJewelry.Content.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses;
using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;
using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;
using System.Collections.Generic;
using Terraria;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

internal class JewelPlayer : ModPlayer
{
    public List<MajorJewelInfo> MajorJewelInfos
    {
        get
        {
            List<MajorJewelInfo> infos = [];

            foreach (var item in jewelry)
                foreach (var info in item.Info)
                    if (info is MajorJewelInfo major)
                        infos.Add(major);

            return infos;
        }
    }

    public List<BasicJewelry> jewelry = new();

    private bool _jumpFlag = false;

    public override void ResetEffects()
    {
        jewelry.Clear();
    }

    public override void PostUpdateEquips()
    {
        foreach (var item in jewelry)
            item.ApplyConstantTrigger(Player);

        Player.GetModPlayer<MaterialPlayer>().StaticMaterialEffects();
        var hallowedPlayer = Player.GetModPlayer<HallowedBonus.HallowedBonusPlayer>();
        hallowedPlayer.fiveSetPower = 1;

        if (hallowedPlayer.fiveSet)
        {
            foreach (var item in jewelry)
                foreach (var info in item.Info)
                    hallowedPlayer.fiveSetPower += info is MajorJewelInfo ? 0.01f : 0.005f;
        }

        foreach (var item in jewelry)
        {
            item.ApplySingleJewelBonus(Player);
            item.ApplyTo(Player, -(1f - ((float)item.tier + 1f) / 5f), hallowedPlayer.fiveSetPower);
            item.ResetSingleJewelBonus(Player);
        }

        if (Player.jump > 0)
        {
            if (!_jumpFlag)
                JumpEffects();

            _jumpFlag = true;
        }
        else
            _jumpFlag = false;
    }

    private void JumpEffects()
    {
        foreach (var item in MajorJewelInfos)
            item.InstantTrigger(TriggerContext.OnJump, Player);
    }

    public override void OnExtraJumpStarted(ExtraJump jump, ref bool playSound) { }// => JumpEffects();

    public override void OnHurt(Player.HurtInfo info)
    {
        foreach (var item in MajorJewelInfos)
            item.InstantTrigger(TriggerContext.OnTakeDamage, Player);
    }

    private void TriggerOnHit(NPC npc)
    {
        if (npc.immortal)
            return;

        foreach (var item in MajorJewelInfos)
            item.InstantTrigger(TriggerContext.OnHitEnemy, Player);
    }

    public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone) => TriggerOnHit(target);

    public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (proj.GetGlobalProjectile<TriggerGlobalProjectile>().FromTrigger)
            return;

        TriggerOnHit(target);
    }

    private class JewelPlayerItem : GlobalItem
    {
        public override void OnConsumeMana(Item item, Player player, int manaConsumed)
        {
            foreach (var trigger in player.GetModPlayer<JewelPlayer>().MajorJewelInfos)
                trigger.InstantTrigger(TriggerContext.OnUseMana, player);

            if (player.statMana - manaConsumed <= 0)
                foreach (var trigger in player.GetModPlayer<JewelPlayer>().MajorJewelInfos)
                    trigger.InstantTrigger(TriggerContext.OnRunOutOfMana, player);
        }
    }
}
