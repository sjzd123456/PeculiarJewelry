using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;
using System;

namespace PeculiarJewelry.Content.JewelryMechanic.Buffs;

internal class Iron5SetDefenseBonus : ModBuff
{
    public override void Update(Player player, ref int buffIndex) => player.statDefense += GetDefense(player, buffIndex);

    private static int GetDefense(Player player, int buffIndex)
    {
        var bonusPlayer = player.GetModPlayer<IronBonus.IronBonusPlayer>();
        bonusPlayer.effectiveExtraDefense = (int)MathHelper.Lerp(0, bonusPlayer.extraDefense, player.buffTime[buffIndex] / (float)bonusPlayer.maxBuffTime);
        return bonusPlayer.effectiveExtraDefense;
    }

    public override bool ReApply(Player player, int time, int buffIndex)
    {
        if (player.HasBuff<Iron5SetDefenseBonus>())
        {
            player.buffTime[buffIndex] += time;
            player.buffTime[buffIndex] = Math.Clamp(player.buffTime[buffIndex], 0, (int)(7.5f * 60));
        }

        player.GetModPlayer<IronBonus.IronBonusPlayer>().maxBuffTime = player.buffTime[buffIndex];
        return true;
    }

    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        rare = ItemRarityID.Green;
        tip = tip.Replace("{0}", Main.LocalPlayer.GetModPlayer<IronBonus.IronBonusPlayer>().effectiveExtraDefense.ToString());
    }
}
