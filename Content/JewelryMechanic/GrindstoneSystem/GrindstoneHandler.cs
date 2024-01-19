using Terraria.Audio;
using Terraria.DataStructures;

namespace PeculiarJewelry.Content.JewelryMechanic.GrindstoneSystem;

internal class GrindstoneHandler
{
    public static void Input(int i, int j)
    {
        var plr = Main.LocalPlayer;

        if (!plr.ItemAnimationJustStarted) // Stop if past initial frame of use
            return;

        if (plr.DistanceSQ(new Vector2(i, j).ToWorldCoordinates()) > 96 * 96) // Stop if too far away
            return;

        if (plr.HeldItem.ModItem is not IGrindableItem grind || !grind.CanGrind(i, j)) // Stop if not holding a grindable item
            return;

        if (grind.GrindstoneUse(i, j, new EntitySource_TileInteraction(Main.LocalPlayer, i, j, "Grindstone"))) // Use grindstone, only create fx if successful
        {
            for (int x = 0; x < 5; ++x)
                Dust.NewDust(Main.MouseWorld, 1, 1, DustID.MinecartSpark, Main.rand.NextFloat(-2, 2), Main.rand.NextFloat(-2, 2), Scale: 7f);

            SoundEngine.PlaySound(SoundID.Item52, new Vector2(i, j) * 16);
        }
    }
}
