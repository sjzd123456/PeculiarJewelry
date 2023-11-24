namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class TungstenRing : BaseRing
{
    public override string MaterialCategory => "Tungsten";
    protected override int Material => ItemID.TungstenBar;
}