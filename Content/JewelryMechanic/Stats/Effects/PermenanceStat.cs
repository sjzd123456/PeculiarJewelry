using PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Effects;

internal class PermenanceStat : JewelStatEffect
{
    public override StatType Type => StatType.Permenance;
    public override Color Color => new(70, 70, 70);

    public override void Apply(Player player, float strength) => player.statDefense += (int)GetEffectValue(strength, player);
    public override float GetEffectValue(float multiplier, Player player) 
        => (int)(PeculiarJewelry.StatConfig.PermenanceStrength * multiplier * player.MaterialBonus("Iron", Type) * player.MaterialBonus("Demonite", Type));
}
