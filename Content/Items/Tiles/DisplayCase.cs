using PeculiarJewelry.Content.Items.JewelryItems;
using PeculiarJewelry.Content.Items.JewelryItems.Brooches;
using PeculiarJewelry.Content.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Misc;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;
using Terraria.UI.Chat;

namespace PeculiarJewelry.Content.Items.Tiles;

public class DisplayCase : ModItem
{
    public override void SetStaticDefaults() => ItemID.Sets.DisableAutomaticPlaceableDrop[Type] = true;

    public override void SetDefaults()
    {
        Item.DefaultToPlaceableTile(ModContent.TileType<DisplayCaseTile>());
        Item.Size = new(62, 46);
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.maxStack = 1;
        Item.value = Item.buyPrice(silver: 10);
    }
}

public class DisplayCaseTile : ModTile
{
    private static Item HoverItem = null;

    public override void Load() => On_Main.DrawMouseOver += Main_DrawMouseOver;

    private void Main_DrawMouseOver(On_Main.orig_DrawMouseOver orig, Main self)
    {
        orig(self);

        if (HoverItem is null)
            return;

        Point target = new(Player.tileTargetX, Player.tileTargetY);

        if (Main.tile[target].TileType == Type)
        {
            string[] lines = Utils.WordwrapString(GetTooltipLines(), FontAssets.MouseText.Value, 460, 10, out int lineAmount);
            lineAmount++;

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.UIScaleMatrix);
            PlayerInput.SetZoom_UI();

            float textWidth = 0f;

            for (int l = 0; l < lineAmount; l++)
            {
                float x = FontAssets.MouseText.Value.MeasureString(lines[l]).X;

                if (textWidth < x)
                    textWidth = x;
            }

            if (textWidth > 460f)
                textWidth = 460f;

            bool opaque = Main.SettingsEnabled_OpaqueBoxBehindTooltips;
            Vector2 position = Main.MouseScreen + new Vector2(16f);

            if (opaque)
                position += new Vector2(8f, 2f);

            if (position.Y > (Main.screenHeight - 30 * lineAmount))
                position.Y = Main.screenHeight - 30 * lineAmount;

            if (position.X > Main.screenWidth - textWidth)
                position.X = Main.screenWidth - textWidth;

            position.X -= textWidth / 2;

            Color color = new(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor);

            if (opaque)
            {
                color = Color.Lerp(color, Color.White, 1f);
                var size = new Rectangle((int)position.X - 10, (int)position.Y - 5, (int)textWidth + 10 * 2, 30 * lineAmount + 5 + 5 / 2);
                Utils.DrawInvBG(Main.spriteBatch, size, new Color(23, 25, 81, 255) * 0.925f * 0.85f);
            }

            for (int m = 0; m < lineAmount; m++)
                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, lines[m], new Vector2(position.X, position.Y + (m * 30)), color, 0f, Vector2.Zero, Vector2.One);

            Main.mouseText = true;
        }

        HoverItem = null;
    }

    private static string GetTooltipLines()
    {
        List<TooltipLine> lines = new() { new TooltipLine(ModLoader.GetMod("PeculiarJewelry"), "ItemName", Lang.GetItemName(HoverItem.type).Value) };
        HoverItem.ModItem.ModifyTooltips(lines);

        string value = string.Empty;

        foreach (var item in lines)
            value += item.Text + "\n";

        return value[..^2];
    }

    public override void SetStaticDefaults()
    {
        Main.tileFrameImportant[Type] = true;
        Main.tileNoAttach[Type] = true;
        Main.tileLavaDeath[Type] = true;
        Main.tileSolidTop[Type] = true;
        Main.tileTable[Type] = true;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4);
        TileObjectData.newTile.Width = 4;
        TileObjectData.newTile.Height = 4;
        TileObjectData.newTile.CoordinateHeights = [16, 16, 16, 18];
        TileObjectData.newTile.Origin = new Point16(2, 3);
        TileObjectData.addTile(Type);

        DustType = DustID.Stone;

        AddMapEntry(new Color(80, 91, 91));
        RegisterItemDrop(ModContent.ItemType<DisplayCase>());
    }

    public override void PlaceInWorld(int i, int j, Item item)
    {
        Tile tile = Main.tile[i, j];
        i -= tile.TileFrameX / 18;
        j -= tile.TileFrameY / 18;
        ModContent.GetInstance<DisplayCaseTE>().Place(i, j);
    }

    public override bool RightClick(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        i -= tile.TileFrameX / 18;
        j -= tile.TileFrameY / 18;
        var entity = TileEntity.ByPosition[new(i, j)] as DisplayCaseTE;

        if (Main.LocalPlayer.HeldItem.ModItem is not BasicJewelry && Main.LocalPlayer.HeldItem.ModItem is not Jewel)
        {
            if (entity.item is not null)
            {
                Main.LocalPlayer.QuickSpawnItemDirect(new EntitySource_TileInteraction(Main.LocalPlayer, i, j), entity.item, entity.item.stack);
                entity.item = null;
                return true;
            }

            return false;
        }

        if (entity.item is not null)
            Main.LocalPlayer.QuickSpawnItemDirect(new EntitySource_TileInteraction(Main.LocalPlayer, i, j), entity.item, entity.item.stack);

        entity.item = Main.LocalPlayer.HeldItem.Clone();
        Main.LocalPlayer.HeldItem.TurnToAir();
        Main.mouseItem.TurnToAir();
        return true;
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        Tile tile = Main.tile[i, j];
        i -= tile.TileFrameX / 18;
        j -= tile.TileFrameY / 18;
        var entity = TileEntity.ByPosition[new(i, j)] as DisplayCaseTE;

        if (entity.item is not null)
            Item.NewItem(new EntitySource_TileInteraction(Main.LocalPlayer, i, j), new Vector2(i + 2, j + 2).ToWorldCoordinates(), entity.item);

        entity.Kill(i, j);
    }

    public override void MouseOver(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        i -= tile.TileFrameX / 18;
        j -= tile.TileFrameY / 18;
        var entity = TileEntity.ByPosition[new(i, j)] as DisplayCaseTE;
        HoverItem = entity.item;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];

        if (tile.TileFrameX != 54 || tile.TileFrameY != 54)
            return;

        Item item = (TileEntity.ByPosition[new(i - 3, j - 3)] as DisplayCaseTE).item;

        if (item == null) 
            return;

        float rot = 0;
        float scale = 1;

        Vector2 off = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
        item.position = new Vector2(i - 1.5f, j - 1.8f).ToWorldCoordinates() + off;
        item.position -= item.Size / 2f;

        if (item.Size.Y < 30)
            item.position += new Vector2(0, 30 - item.Size.Y);

        Color lightColor = Lighting.GetColor(i, j);

        if (item.ModItem.PreDrawInWorld(spriteBatch, lightColor, Color.White, ref rot, ref scale, Main.maxItems))
        {
            Texture2D tex = TextureAssets.Item[item.type].Value;
            spriteBatch.Draw(tex, (item.position - Main.screenPosition).Floor(), null, lightColor, rot, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }

        item.ModItem.PostDrawInWorld(spriteBatch, lightColor, Color.White, rot, scale, Main.maxItems);
        return;
    }
}

public class DisplayCaseTE : ModTileEntity
{
    public override bool IsTileValidForEntity(int x, int y) => Main.tile[x, y].HasTile && Main.tile[x, y].TileType == ModContent.TileType<DisplayCaseTile>();

    public Item item = null;

    public override void SaveData(TagCompound tag)
    {
        if (item is not null)
            tag.Add(nameof(item), item);
    }

    public override void LoadData(TagCompound tag)
    {
        if (tag.ContainsKey(nameof(item)))
            item = tag.Get<Item>(nameof(item));
    }
}
