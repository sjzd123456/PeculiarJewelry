using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;

public abstract class Jewel : ModItem
{
    protected abstract Type InfoType { get; }

    JewelInfo info;

    public sealed override void SetDefaults()
    {
        info = Activator.CreateInstance(InfoType) as JewelInfo;
        info.Setup();

        Defaults();
    }

    public abstract void Defaults();

    public sealed override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var name = tooltips.First(x => x.Name == "ItemName");
        name.Text = info.GetTierText() + " " + name.Text;
    }

    public sealed override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        JewelDrawing.DrawJewel(TextureAssets.Item[Type], position, Item.Size / 2f, drawColor, 0f, 32f / Item.width, Item.width, Item.height + 2, info);
        return false;
    }

    public sealed override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
    {
        JewelDrawing.DrawJewel(TextureAssets.Item[Type], Item.Center - Main.screenPosition, Item.Size / 2f, lightColor, rotation, scale, Item.width, Item.height + 2, info);
        return false;
    }
}
