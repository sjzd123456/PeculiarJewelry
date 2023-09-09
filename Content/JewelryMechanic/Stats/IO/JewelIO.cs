using System;
using Terraria.ModLoader.IO;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.IO;

internal static class JewelIO
{
    public static TagCompound SaveAs(this JewelInfo info) => SaveInfo(info);

    private static TagCompound SaveInfo(JewelInfo info)
    {
        TagCompound tag = new()
        {
            //Misc info
            { "infoType", info.GetType().AssemblyQualifiedName },
            { "infoTier", (byte)info.tier },
            { "infoExclusivity", (byte)info.exclusivity },
            { "infoCuts", (byte)info.cuts },

            //Stats
            { "infoMajor", info.Major.SaveAs() }
        };

        for (int i = 0; i < info.SubStats.Count; i++)
            tag.Add("infoSub" + i, info.SubStats[i].SaveAs());

        return tag;
    }

    public static JewelInfo LoadInfo(TagCompound tag)
    {
        string type = tag.GetString("infoType");
        JewelInfo info = Activator.CreateInstance(Type.GetType(type)) as JewelInfo;

        info.tier = (JewelInfo.JewelTier)tag.GetByte("infoTier");
        info.exclusivity = (StatExclusivity)tag.GetByte("infoExclusivity");
        info.cuts = tag.GetByte("infoCuts");
        info.SetupFromIO(LoadStat(tag.GetCompound("infoMajor")));

        for (int i = 0; i < info.SubStats.Count; i++)
        {
            if (tag.TryGet("infoSub" + i, out TagCompound sub))
                info.SubStats.Add(LoadStat(sub));
            else
                break;
        }
        return info;
    }

    public static TagCompound SaveAs(this JewelStat stat) => SaveStat(stat);

    private static TagCompound SaveStat(JewelStat stat)
    {
        TagCompound tag = new()
        {
            { "statType", (byte)stat.Type },
            { "statStrength", stat.Strength }
        };
        return tag;
    }

    private static JewelStat LoadStat(TagCompound tag)
    {
        JewelStat stat = new((StatType)tag.GetByte("statType"))
        {
            Strength = tag.GetFloat("statStrength")
        };
        return stat;
    }
}
