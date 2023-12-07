namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class TitaniumRing : BaseRing
{
    public override string MaterialCategory => "Titanium";
    protected override int Material => ItemID.TitaniumBar;
    protected override bool Hardmode => true;
}