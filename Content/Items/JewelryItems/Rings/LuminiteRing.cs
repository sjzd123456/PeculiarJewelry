namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class LuminiteRing : BaseRing
{
    public override string MaterialCategory => "Luminite";
    protected override int Material => ItemID.LunarBar;
    protected override bool Hardmode => true;
}