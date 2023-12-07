namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class LeadRing : BaseRing
{
    public override string MaterialCategory => "Lead";
    protected override int Material => ItemID.LeadBar;
}