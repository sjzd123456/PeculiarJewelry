namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class CrimtaneRing : BaseRing
{
    public override string MaterialCategory => "Crimtane";
    protected override int Material => ItemID.CrimtaneBar;
}