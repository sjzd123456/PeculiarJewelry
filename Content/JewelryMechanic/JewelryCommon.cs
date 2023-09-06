using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;

namespace PeculiarJewelry.Content.JewelryMechanic;

internal class JewelryCommon
{
    public static JewelryStatConfig Config => ModContent.GetInstance<JewelryStatConfig>();

    public static int MajorMinorType() => Main.rand.NextFloat() <= Config.ChanceForMajor ? ModContent.ItemType<MajorJewel>() : ModContent.ItemType<MinorJewel>();
    public static float StatStrengthRange()
    {
        float result = Config.GlobalPowerScale - (Config.GlobalPowerScaleSteps * Main.rand.Next(3));

        if (result < 0)
            return 0;
        return result;
    }
}
