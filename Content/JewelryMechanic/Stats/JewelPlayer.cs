﻿using PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses;
using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;
using System.Collections.Generic;
using Terraria;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

internal class JewelPlayer : ModPlayer
{
    private List<MajorJewelInfo> MajorJewelInfos
    {
        get
        {
            List<MajorJewelInfo> infos = new();

            foreach (var item in jewelInfos)
                if (item is MajorJewelInfo major)
                    infos.Add(major);

            return infos;
        }
    }

    public List<JewelInfo> jewelInfos = new();

    public override void ResetEffects()
    {
        jewelInfos.Clear();
    }

    public override void PostUpdateEquips()
    {
        foreach (var item in MajorJewelInfos)
            item.ConstantTrigger(Player);

        Player.GetModPlayer<MaterialPlayer>().StaticMaterialEffects();

        foreach (var item in jewelInfos)
            item.ApplyTo(Player);
    }

    public override void OnHurt(Player.HurtInfo info)
    {
        foreach (var item in MajorJewelInfos)
            item.InstantTrigger(TriggerContext.OnTakeDamage, Player);
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        foreach (var item in MajorJewelInfos)
            item.InstantTrigger(TriggerContext.OnHitEnemy, Player);
    }
}
