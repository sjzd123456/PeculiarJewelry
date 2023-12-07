namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class PalladiumRing : BaseRing
{
    public override string MaterialCategory => "Palladium";
    protected override int Material => ItemID.PalladiumBar;
    protected override bool Hardmode => true;
}