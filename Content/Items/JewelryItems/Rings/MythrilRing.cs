namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class MythrilRing : BaseRing
{
    public override string MaterialCategory => "Mythril";
    protected override int Material => ItemID.MythrilBar;
    protected override bool Hardmode => true;
}