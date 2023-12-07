namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class OrichalcumRing : BaseRing
{
    public override string MaterialCategory => "Orichalcum";
    protected override int Material => ItemID.OrichalcumBar;
    protected override bool Hardmode => true;
}