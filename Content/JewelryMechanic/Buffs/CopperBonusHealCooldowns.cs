namespace PeculiarJewelry.Content.JewelryMechanic.Buffs;

internal class LifeCrystalCopper5SetCooldown : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }

    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare) => rare = ItemRarityID.Red;
}

internal class LifeFruitCopper5SetCooldown : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.debuff[Type] = true;
        BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
    }

    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare) => rare = ItemRarityID.Red;
}
