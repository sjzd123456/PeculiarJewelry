namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class ChlrophyteRing : BaseRing
{
    public override string MaterialCategory => "Chlorophyte";
    protected override int Material => ItemID.ChlorophyteBar;
    protected override bool Hardmode => true;
}