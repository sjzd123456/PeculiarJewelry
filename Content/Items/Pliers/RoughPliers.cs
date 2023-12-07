namespace PeculiarJewelry.Content.Items.Pliers;

class RoughPliers : Plier
{
    public override void SetDefaults()
    {
        Item.width = 28;
        Item.height = 28;
        Item.knockBack = 2;
        Item.autoReuse = true;
        Item.useTime = Item.useAnimation = 30;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.UseSound = SoundID.Item1;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 2;
        Item.crit = -4;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(silver: 20);
    }

    public override bool SuccessfulAttempt() => Main.rand.NextBool(4);
}
