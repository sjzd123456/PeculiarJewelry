namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class AdamantiteRing : BaseRing
{
    public override string MaterialCategory => "Adamantite";
    protected override int Material => ItemID.AdamantiteBar;
    protected override bool Hardmode => true;
}