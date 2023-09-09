using System;
using Terraria.ModLoader.IO;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.IO;

internal static class JewelIO
{
    public static void SaveTo(this JewelInfo info, TagCompound tag) => SaveInfo(info, tag);

    private static void SaveInfo(JewelInfo info, TagCompound tag)
    {
        //Misc info
        tag.Add("infoType", info.GetType().AssemblyQualifiedName);
        tag.Add("infoTier", (byte)info.tier);
        tag.Add("infoExclusivity", (byte)info.exclusivity);
        tag.Add("infoCuts", (byte)info.cuts);

        //Stats
        tag.Add("infoMajor", info.Major.SaveAs());

        for (int i = 0; i < info.SubStats.Count; i++)
            tag.Add("infoSub" + i, info.SubStats[i].SaveAs());
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
            { "statStrength", (Half)stat.Strength }
        };
        return tag;
    }

    private static JewelStat LoadStat(TagCompound tag)
    {
        JewelStat stat = new((StatType)tag.GetByte("statType"))
        {
            Strength = (float)tag.Get<Half>("statStrength")
        };
        return stat;
    }
}
