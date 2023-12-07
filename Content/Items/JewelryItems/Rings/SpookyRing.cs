namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class SpookyRing : BaseRing
{
    public override string MaterialCategory => "Spooky";
    protected override int Material => ItemID.SpookyWood;
    protected override bool Hardmode => true;
}