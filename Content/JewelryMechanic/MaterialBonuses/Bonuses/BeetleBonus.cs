using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class BeetleBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Beetle";

    float bonus = 1f;

    public override bool AppliesToStat(Player player, StatType type) 
        => type == StatType.Tenacity || type == StatType.Permenance || type == StatType.Celerity || type == StatType.Dexterity;
    public override void SingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1.25f;
    public override void ResetSingleJewelBonus(Player player, BasicJewelry jewel) => bonus = 1f;

    public override float EffectBonus(Player player, StatType type)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);
        bool movement = type == StatType.Celerity || type == StatType.Dexterity;

        if (count >= 1)
            return movement ? bonus : 0.94f;
        return 1f;
    }

    public override void StaticBonus(Player player, bool firstSet)
    {
        if (CountMaterial(player) >= 3 && player.velocity.LengthSquared() < 0.01f)
            player.GetDamage(DamageClass.Generic) += 1;
    }

    // Needs 5-Set
}
