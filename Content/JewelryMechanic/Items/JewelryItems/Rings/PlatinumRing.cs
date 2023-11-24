namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class PlatinumRing : BaseRing
{
    public override string MaterialCategory => "Platinum";
    protected override int Material => ItemID.PlatinumBar;
}