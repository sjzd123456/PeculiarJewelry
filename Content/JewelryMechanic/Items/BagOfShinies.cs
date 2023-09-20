using PeculiarJewelry;
using PeculiarJewelry.Content.JewelryMechanic;
using PeculiarJewelry.Content.JewelryMechanic.Items;
using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Misc;
using PeculiarJewelry.Content.JewelryMechanic.NPCs;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;

namespace PeculiarJewelry.Content.JewelryMechanic.Items;

class BagOfShinies : ModItem
{
    public JewelTier tier;

    public override void SetDefaults()
    {
        Item.Size = new(32, 34);
        Item.rare = ModContent.RarityType<JewelRarity>();
    }

    public override bool CanRightClick()
    {
        Main.LocalPlayer.GetModPlayer<StupidIdiotItemLootWorkaroundPlayer>().storedTier = tier;
        return true;
    }

    public override void ModifyItemLoot(ItemLoot itemLoot)
    {
        itemLoot.AddCommon<SparklyDust>(5);

        // Not expert rule
        LeadingConditionRule notExpert = new(new Conditions.NotExpert());
        var majorRule = ItemDropRule.Common(ModContent.ItemType<MajorJewel>(), (int)Math.Round(1 / ModContent.GetInstance<JewelryStatConfig>().ChanceForMajor));
        majorRule.OnFailedRoll(ItemDropRule.Common(ModContent.ItemType<MinorJewel>(), 1));
        notExpert.OnSuccess(majorRule);
        notExpert.OnSuccess(SupportItemDrops(20));
        itemLoot.Add(notExpert);

        // Expert rule
        LeadingConditionRule expert = new(new Conditions.IsExpert());
        expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MajorJewel>(), 2));
        expert.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MajorJewel>(), 1, 1, 3));
        notExpert.OnSuccess(SupportItemDrops(20));
        itemLoot.Add(expert);

        // Master rule
        LeadingConditionRule notExpertForMaster = new(new Conditions.NotExpert());
        LeadingConditionRule master = new(new Conditions.IsMasterMode());
        master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MajorJewel>(), 2, 1, 2));
        master.OnSuccess(ItemDropRule.Common(ModContent.ItemType<MajorJewel>(), 1, 1, 4));
        notExpertForMaster.OnSuccess(master);
        itemLoot.Add(notExpertForMaster);
    }

    private IItemDropRule SupportItemDrops(int chance)
    {
        return ItemDropRule.OneFromOptions(chance);
    }

    public override void OnSpawn(IEntitySource source)
    {
        if (source is EntitySource_Loot loot && loot.Entity is NPC npc && npc.boss)
            tier = (JewelTier)BossLootGlobal.GetBossTier(npc);
    }
}

class StupidIdiotItemLootWorkaroundPlayer : ModPlayer
{
    internal JewelTier storedTier;
}
