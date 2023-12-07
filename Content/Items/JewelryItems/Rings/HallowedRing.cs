namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class HallowedRing : BaseRing
{
    public override string MaterialCategory => "Hallowed";
    protected override int Material => ItemID.HallowedBar;
    protected override bool Hardmode => true;
}