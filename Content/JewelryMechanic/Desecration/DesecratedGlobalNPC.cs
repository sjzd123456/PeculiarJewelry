using System.Collections.Generic;
using System.Reflection;
using Terraria.GameContent.ItemDropRules;

namespace PeculiarJewelry.Content.JewelryMechanic.Desecration;

internal class DesecratedGlobalNPC : GlobalNPC
{
    private static FieldInfo DatabaseField;
    private static MethodInfo ResolveRuleMethod;

    public override void Load()
    {
        On_ItemDropResolver.TryDropping += MultiplyRules;

        DatabaseField = typeof(ItemDropResolver).GetField("_database", BindingFlags.NonPublic | BindingFlags.Instance);
        ResolveRuleMethod = typeof(ItemDropResolver).GetMethod("ResolveRule", BindingFlags.NonPublic | BindingFlags.Instance);
    }

    private void MultiplyRules(On_ItemDropResolver.orig_TryDropping orig, ItemDropResolver self, DropAttemptInfo info)
    {
        orig(self, info);
        float profanity = DesecratedSystem.TotalProfanity;

        if (profanity <= 0 || info.npc is null)
            return;

        var database = DatabaseField.GetValue(self) as ItemDropDatabase;
        List<IItemDropRule> rulesForNPCID = database.GetRulesForNPCID(info.npc.netID);
        float percentage = DesecratedSystem.LootScaleFactor;
        int count = (int)(rulesForNPCID.Count * percentage);

        for (int i = 0; i < count; ++i)
        {
            int ruleIndex = Main.rand.Next(rulesForNPCID.Count);
            var result = (ItemDropAttemptResult)ResolveRuleMethod.Invoke(self, [rulesForNPCID[ruleIndex], info]);
        }
    }
}
