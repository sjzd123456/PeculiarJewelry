using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses;
using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;
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

            foreach (var item in jewelry)
                foreach (var info in item.Info)
                    if (info is MajorJewelInfo major)
                        infos.Add(major);

            return infos;
        }
    }

    public List<BasicJewelry> jewelry = new();

    public override void ResetEffects()
    {
        jewelry.Clear();
    }

    public override void PostUpdateEquips()
    {
        foreach (var item in jewelry)
            item.ApplyConstantTrigger(Player);

        Player.GetModPlayer<MaterialPlayer>().StaticMaterialEffects();
        Player.GetModPlayer<HallowedBonus.HallowedBonusPlayer>().fiveSetPower = 1;

        if (Player.GetModPlayer<HallowedBonus.HallowedBonusPlayer>().fiveSet)
        {
            foreach (var item in jewelry)
                foreach (var info in item.Info)
                    Player.GetModPlayer<HallowedBonus.HallowedBonusPlayer>().fiveSetPower += info is MajorJewelInfo ? 0.01f : 0.005f;
        }

        foreach (var item in jewelry)
        {
            item.ApplySingleJewelBonus(Player);
            item.ApplyTo(Player, -(1f - ((float)item.tier + 1f) / 5f), Player.GetModPlayer<HallowedBonus.HallowedBonusPlayer>().fiveSetPower);
            item.ResetSingleJewelBonus(Player);
        }
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
