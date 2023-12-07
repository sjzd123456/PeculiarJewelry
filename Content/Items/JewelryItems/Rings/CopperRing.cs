namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class CopperRing : BaseRing
{
    public override string MaterialCategory => "Copper";
    protected override int Material => ItemID.CopperBar;
}