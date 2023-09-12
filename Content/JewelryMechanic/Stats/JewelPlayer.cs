using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;
using System.Collections.Generic;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats;

internal class JewelPlayer : ModPlayer
{
    public List<MajorJewelInfo> majorInfo = new();

    public override void ResetEffects()
    {
        majorInfo.Clear();
    }

    public override void PostUpdateEquips()
    {
        foreach (var item in majorInfo)
            item.ConstantTrigger(Player);
    }

    public override void OnHurt(Player.HurtInfo info)
    {
        foreach (var item in majorInfo)
            item.InstantTrigger(TriggerContext.OnTakeDamage, Player);
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        foreach (var item in majorInfo)
            item.InstantTrigger(TriggerContext.OnHitEnemy, Player);
    }
}
