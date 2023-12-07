namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class ShroomiteRing : BaseRing
{
    public override string MaterialCategory => "Shroomite";
    protected override int Material => ItemID.ShroomiteBar;
    protected override bool Hardmode => true;
}