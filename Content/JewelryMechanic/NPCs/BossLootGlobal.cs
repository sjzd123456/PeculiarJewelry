using PeculiarJewelry.Content.JewelryMechanic.Items;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.ItemDropRules;

namespace PeculiarJewelry.Content.JewelryMechanic.NPCs;

internal class BossLootGlobal : GlobalNPC
{
    public static int GetBossTier(NPC npc)
    {
        Mod checklist = ModLoader.GetMod("BossChecklist");
        Dictionary<string, Dictionary<string, object>> dict = (Dictionary<string, Dictionary<string, object>>)checklist.Call("GetBossInfoDictionary", ModLoader.GetMod("PeculiarJewelry"), "0.0.0.0");
        var index = dict.Keys.First(x => (dict[x]["npcIDs"] as List<int>).Contains(npc.type));
        float progression = (float)dict[index]["progression"];

        if (progression == (int)progression)
            return (int)progression;
        else
            return Main.rand.NextFloat() < progression % 1 ? (int)progression + 1 : (int)progression;
    }

    public override void ModifyGlobalLoot(GlobalLoot globalLoot)
    {
        globalLoot.Add(ItemDropRule.ByCondition(new Conditions.LegacyHack_IsABoss(), ModContent.ItemType<BagOfShinies>()));
    }
}
