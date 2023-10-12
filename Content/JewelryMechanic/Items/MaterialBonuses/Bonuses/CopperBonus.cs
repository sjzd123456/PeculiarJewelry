namespace PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;

internal class CopperBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Copper";

    public override float EffectBonus(Player player)
    {
        int copperCount = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (copperCount >= 1)
            return 1.15f;
        return 1f;
    }

    public override void StaticBonus(Player player)
    {
        int copperCount = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (copperCount >= 3)
            player.statLifeMax2 += player.statDefense;
    }

    // Needs 5-Set
}
