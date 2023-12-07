using System.Linq;
using Terraria.DataStructures;

namespace PeculiarJewelry.Content.JewelryMechanic.Globals;

internal class JewelGlobalItem : GlobalItem
{
    private static int[] GemOres = System.Array.Empty<int>();

    public override void Load()
    {
        GemOres = new int[] { TileID.Ruby, TileID.Amethyst, TileID.Emerald, TileID.Ruby, TileID.Diamond, TileID.Topaz, TileID.Sapphire };
    }

    public override void OnSpawn(Item item, IEntitySource source)
    {
        if (source is EntitySource_TileBreak tileBreak && GemOres.Contains(Main.tile[tileBreak.TileCoords].TileType))
        {
            if (Main.rand.NextBool(60))
            {
                item.SetDefaults(JewelryCommon.MajorMinorType());
                return;
            }
        }
    }
}
