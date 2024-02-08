using PeculiarJewelry.Content.Items;
using Terraria.GameContent.ItemDropRules;

namespace PeculiarJewelry.Content.JewelryMechanic;

internal class TierCutoffCondition(int tier) : IItemDropRuleCondition
{
    private readonly int _desiredTier = tier;

    public bool CanDrop(DropAttemptInfo info) => (int)info.player.GetModPlayer<StupidIdiotItemLootWorkaroundPlayer>().storedTier > _desiredTier;
    public bool CanShowItemDropInUI() => true;
    public string GetConditionDescription() => Language.GetTextValueWith("Mods.PeculiarJewelry.TierCutoffCondition", _desiredTier);
}
