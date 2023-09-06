using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;

public abstract class Jewel : ModItem
{
    protected abstract Type InfoType { get; }

    JewelInfo info;

    public sealed override void SetDefaults()
    {
        info = Activator.CreateInstance(InfoType) as JewelInfo;
        info.Setup(0); //Info is by default 

        Defaults();
    }

    public abstract void Defaults();

    public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        static string Localize(string text) => Language.GetTextValue("Mods.PeculiarJewelry." + text);

        var name = tooltips.First(x => x.Name == "ItemName");
        name.Text = Localize("Items." + Name + ".Prefix") + " " + info.GetTierText() + " " + Localize("Jewelry.Jewel") + " of " + info.Major.GetName().Value;
        name.OverrideColor = info.Major.Get().Color;

        tooltips.Add(new TooltipLine(Mod, "MajorStat", Localize("Jewelry.Main") + ": " + info.Major.GetDescription().Value) { OverrideColor = info.Major.Get().Color });

        var subStatTooltips = info.SubStatTooltips();

        for (int i = 0; i < subStatTooltips.Length; ++i)
            tooltips.Add(new TooltipLine(Mod, "SubStat" + i, subStatTooltips[i]) { OverrideColor = info.SubStats[i].Get().Color });

        if (info.exclusivity != StatExclusivity.None)
            tooltips.Add(new TooltipLine(Mod, "StatExclusivity", info.exclusivity.Localize()));
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
        info.tier = JewelInfo.JewelTier.Mythical3;
        return false;
    }
}
