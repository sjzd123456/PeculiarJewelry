using PeculiarJewelry.Content.Items.JewelryItems;
using PeculiarJewelry.Content.Items.JewelryItems.Brooches;
using PeculiarJewelry.Content.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic;
using PeculiarJewelry.Content.JewelryMechanic.Misc;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using PeculiarJewelry.Content.NPCs;
using System;
using System.Reflection.Metadata;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ObjectData;
using Verdant.Items;

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

        entity.item = Main.LocalPlayer.HeldItem.Clone();
        Main.LocalPlayer.HeldItem.TurnToAir();
        Main.mouseItem.TurnToAir();
        return true;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
    {
        Tile tile = Main.tile[i, j];

        if (tile.TileFrameX != 0 || tile.TileFrameY != 0)
            return true;

        Item item = (TileEntity.ByPosition[new(i, j)] as DisplayCaseTE).item;

        if (item == null) 
            return true;

        Color lightColor = Lighting.GetColor(i, j);
        float rot = 0;
        float scale = 1;

        Vector2 off = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
        item.position = new Vector2(i + 1.5f, j + 1.2f).ToWorldCoordinates() + off;
        item.position -= item.Size / 2f;

        if (item.ModItem.PreDrawInWorld(spriteBatch, lightColor, Color.White, ref rot, ref scale, Main.maxItems))
        {
            Texture2D tex = TextureAssets.Item[item.type].Value;
            spriteBatch.Draw(tex, (item.position - Main.screenPosition).Floor(), null, lightColor, rot, Vector2.Zero, 1f, SpriteEffects.None, 0);

        }

        item.ModItem.PostDrawInWorld(spriteBatch, lightColor, Color.White, rot, scale, Main.maxItems);
        return true;
    }
}

public class DisplayCaseTE : ModTileEntity
{
    public override bool IsTileValidForEntity(int x, int y) => Main.tile[x, y].HasTile && Main.tile[x, y].TileType == ModContent.TileType<DisplayCaseTile>();

    public Item item = null;
}
