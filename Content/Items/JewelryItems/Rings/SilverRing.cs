namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class SilverRing : BaseRing
{
    public override string MaterialCategory => "Silver";
    protected override int Material => ItemID.SilverBar;
}