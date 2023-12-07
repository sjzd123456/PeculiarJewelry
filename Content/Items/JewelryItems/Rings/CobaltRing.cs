namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class CobaltRing : BaseRing
{
    public override string MaterialCategory => "Cobalt";
    protected override int Material => ItemID.CobaltBar;
    protected override bool Hardmode => true;
}