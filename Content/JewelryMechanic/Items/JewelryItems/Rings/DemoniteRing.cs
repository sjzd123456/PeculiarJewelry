namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class DemoniteRing : BaseRing
{
    public override string MaterialCategory => "Demonite";
    protected override int Material => ItemID.DemoniteBar;
}