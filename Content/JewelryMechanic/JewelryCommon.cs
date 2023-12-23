using PeculiarJewelry.Content.Items.Jewels;

namespace PeculiarJewelry.Content.JewelryMechanic;

internal class JewelryCommon
{
    public static JewelryStatConfig Config => ModContent.GetInstance<JewelryStatConfig>();

    public static string[] PrehardmodeMetals = "Copper Tin Iron Lead Silver Tungsten Gold Platinum".Split(' ');

    public static int MajorMinorType() => Main.rand.NextFloat() <= Config.ChanceForMajor ? ModContent.ItemType<MajorJewel>() : ModContent.ItemType<MinorJewel>();

    public static float StatStrengthRange()
    {
        if (Config.GlobalPowerScaleMinimum == 1 || Config.PowerScaleStepCount == 1)
            return 1;

        float factor = Main.rand.Next(Config.PowerScaleStepCount) / (float)(Config.PowerScaleStepCount - 1);
        float result = MathHelper.Lerp(Config.GlobalPowerScaleMinimum, 1, factor);

        return result;
    }

    public static int GetRandomJewelryType(string[] materials)
    {
        string[] types = "Anklet Bracelet Brooch Choker Earring Hairpin Ring Tiara".Split(' ');
        string name = nameof(PeculiarJewelry) + "/" + Main.rand.Next(materials) + Main.rand.Next(types);
        return ModContent.Find<ModItem>(name).Type;
    }
}
