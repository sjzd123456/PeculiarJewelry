namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class IronRing : BaseRing
{
    public override string MaterialCategory => "Iron";
    protected override int Material => ItemID.IronBar;
}