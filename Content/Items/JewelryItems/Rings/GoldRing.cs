namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class GoldRing : BaseRing
{
    public override string MaterialCategory => "Gold";
    protected override int Material => ItemID.GoldBar;
}