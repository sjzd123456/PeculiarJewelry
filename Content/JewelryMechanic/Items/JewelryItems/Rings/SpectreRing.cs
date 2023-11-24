namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class SpectreRing : BaseRing
{
    public override string MaterialCategory => "Spectre";
    protected override int Material => ItemID.SpectreBar;
    protected override bool Hardmode => true;
}