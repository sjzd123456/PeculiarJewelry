using PeculiarJewelry.Content.JewelryMechanic.Stats;
using PeculiarJewelry.Content.JewelryMechanic.Stats.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ModLoader.IO;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;

public abstract class Jewel : ModItem
{
    protected abstract Type InfoType { get; }

    public JewelInfo info;

    public sealed override void SetDefaults()
    {
        info = Activator.CreateInstance(InfoType) as JewelInfo;
        info.Setup(JewelInfo.JewelTier.Natural); //Info is tier 0 by default 

        Defaults();
    }

    public abstract void Defaults();

    public sealed override void ModifyTooltips(List<TooltipLine> tooltips) => JewelInfoTooltips(tooltips, info, this);

    /// <summary>
    /// Adds all relevant tooltips to the given list with the given info.
    /// </summary>
    /// <param name="tooltips">Tooltips to modify.</param>
    /// <param name="info">The info to reference.</param>
    /// <param name="modItem">The mod item this is being attached to.</param>
    /// <param name="displayAsJewel">Whether this is being used directly on a Jewel item or as part of a jewelry accessory. 
    /// This ignores the name modification and hides the exclusivity and cuts left.</param>
    public static void JewelInfoTooltips(List<TooltipLine> tooltips, JewelInfo info, ModItem modItem, bool displayAsJewel = true)
    {
        static string Localize(string text) => Language.GetTextValue("Mods.PeculiarJewelry." + text);

        if (displayAsJewel)
        {
            var name = tooltips.First(x => x.Name == "ItemName");
            name.Text = Localize("Items." + modItem.Name + ".Prefix") + " " + info.GetTierText() + " " + Localize("Jewelry.Jewel") + " of " + info.Major.GetName().Value;
            name.OverrideColor = info.Major.Get().Color;

            tooltips.Add(new TooltipLine(modItem.Mod, "JewelTier", Language.GetText("Mods.PeculiarJewelry.Jewelry.TierTooltip").WithFormatArgs((int)info.tier).Value));
        }
        else
        {
            string major = info is MajorJewelInfo ? nameof(MajorJewel) : nameof(MinorJewel);
            string name = Localize("Items." + major + ".Prefix") + " " + info.GetTierText() + " " + Localize("Jewelry.Jewel") + " of " + info.Major.GetName().Value;
            tooltips.Add(new TooltipLine(modItem.Mod, "JewelName", name) { OverrideColor = info.Major.Get().Color });
        }

        if (displayAsJewel || PeculiarJewelry.ShiftDown)
        {
            tooltips.Add(new TooltipLine(modItem.Mod, "MajorStat", "+" + info.Major.GetDescription()) { OverrideColor = info.Major.Get().Color });

            var subStatTooltips = info.SubStatTooltips();

            for (int i = 0; i < subStatTooltips.Length; ++i)
            {
                if (!displayAsJewel && subStatTooltips[i] == "-")
                    continue;

                Color color = i < info.SubStats.Count ? info.SubStats[i].Get().Color : Color.White;
                string text = i < info.SubStats.Count ? "   +" + subStatTooltips[i] : "   " + subStatTooltips[i];
                tooltips.Add(new TooltipLine(modItem.Mod, "SubStat" + i, text) { OverrideColor = color });
            }
        }

        if (displayAsJewel)
        {
            if (info.exclusivity != StatExclusivity.None)
                tooltips.Add(new TooltipLine(modItem.Mod, "StatExclusivity", info.exclusivity.Localize()));

            tooltips.Add(new TooltipLine(modItem.Mod, "JewelCuts", (info.MaxCuts - info.cuts) + "/" + info.MaxCuts + Localize("Jewelry.CutsRemaining")));
        }
    }

    public sealed override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        JewelDrawing.DrawJewel(TextureAssets.Item[Type], position, Item.Size / 2f, info.Major.Get().Color, 0f, 32f / Item.width, Item.width, Item.height + 2, info);
        return false;
    }

    public sealed override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
    {
        Color col = Lighting.GetColor(Item.Center.ToTileCoordinates(), info.Major.Get().Color);
        JewelDrawing.DrawJewel(TextureAssets.Item[Type], Item.Center - Main.screenPosition, Item.Size / 2f, col, rotation, scale, Item.width, Item.height + 2, info);
        return false;
    }

    public override void SaveData(TagCompound tag) => tag.Add("info", info.SaveAs());

    public override void LoadData(TagCompound tag)
    {
        TagCompound infoCompound = tag.GetCompound("info");
        info = JewelIO.LoadInfo(infoCompound);
    }
}
