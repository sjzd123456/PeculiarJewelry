namespace PeculiarJewelry.Content.JewelryMechanic.Items.Pliers;

class GentlePliers : Plier
{
    public override void SetDefaults()
    {
        Item.width = 28;
        Item.height = 28;
        Item.knockBack = 2;
        Item.autoReuse = true;
        Item.useTime = Item.useAnimation = 22;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 6;
        Item.crit = -4;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(gold: 20);
    }

    public override bool SuccessfulAttempt() => !Main.rand.NextBool(4);
}
