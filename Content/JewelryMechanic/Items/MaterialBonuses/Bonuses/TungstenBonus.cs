namespace PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;

internal class TungstenBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Tungsten";

    public override float EffectBonus(Player player)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 1)
            return 1.15f;
        return 1f;
    }

    public override void StaticBonus(Player player)
    {

    }

    // Needs 3-Set, 5-Set
}
