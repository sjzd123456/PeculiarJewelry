namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class MeteoriteRing : BaseRing
{
    public override string MaterialCategory => "Meteorite";
    protected override int Material => ItemID.MeteoriteBar;
}