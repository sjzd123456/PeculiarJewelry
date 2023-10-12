namespace PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;

internal class LeadBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Lead";

    public override float EffectBonus(Player player)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 1)
            return 1.15f;
        return 1f;
    }

    public override void StaticBonus(Player player)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 3)
            player.noKnockback = true;
    }

    // Needs 5-Set
}
