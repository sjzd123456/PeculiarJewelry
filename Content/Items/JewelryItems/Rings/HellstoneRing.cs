namespace PeculiarJewelry.Content.Items.JewelryItems.Rings;

[AutoloadEquip(EquipType.HandsOn)]
public class HellstoneRing : BaseRing
{
    public override string MaterialCategory => "Hellstone";
    protected override int Material => ItemID.HellstoneBar;
}