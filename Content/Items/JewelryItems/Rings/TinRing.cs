namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class TinRing : BaseRing
{
    public override string MaterialCategory => "Tin";
    protected override int Material => ItemID.TinBar;
}