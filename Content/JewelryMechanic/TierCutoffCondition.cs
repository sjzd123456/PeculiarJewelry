using PeculiarJewelry.Content.JewelryMechanic.Items;
using Terraria.GameContent.ItemDropRules;

namespace PeculiarJewelry.Content.JewelryMechanic;

internal class TierCutoffCondition : IItemDropRuleCondition
{
    private readonly int _desiredTier = 0;

    public TierCutoffCondition(int tier)
    {
        _desiredTier = tier;
    }

    public bool CanDrop(DropAttemptInfo info) => (int)info.player.GetModPlayer<StupidIdiotItemLootWorkaroundPlayer>().storedTier > _desiredTier;
    public bool CanShowItemDropInUI() => true;
    public string GetConditionDescription() => $"Only from bags of tier {_desiredTier} or higher.";
}
