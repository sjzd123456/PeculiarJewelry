namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class BeetleRing : BaseRing
{
    public override string MaterialCategory => "Beetle";
    protected override int Material => ItemID.BeetleHusk;
    protected override bool Hardmode => true;
}