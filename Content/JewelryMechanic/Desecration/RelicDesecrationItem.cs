using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace PeculiarJewelry.Content.JewelryMechanic.Desecration;

internal class RelicDesecrationItem : GlobalItem
{
    public override bool InstancePerEntity => true;

    internal float profanity = 0;

    public override bool AppliesToEntity(Item item, bool lateInstantiation) => item.createTile != -1 && RelicTile.RelicTypes.Contains(item.createTile);

    public override void OnSpawn(Item item, IEntitySource source)
    {
        if (source is EntitySource_Loot loot && loot.Entity is NPC npc && npc.boss)
            profanity = DesecratedSystem.TotalProfanity;

        if (source is EntitySource_TileBreak tile)
        {
            int x = tile.TileCoords.X, y = tile.TileCoords.Y;

            if (!RelicTile.IsRelic(ref x, ref y))
                return;

            if (!TileEntity.ByPosition.TryGetValue(new(x, y), out var te) || te is not RelicDesecrationTE relic)
                return;

            profanity = relic.profanity;
        }
    }
}

internal class RelicDesecrationTE : ModTileEntity
{
    internal float profanity = 0;

    public override bool IsTileValidForEntity(int x, int y) => RelicTile.IsRelic(ref x, ref y);
    public override void SaveData(TagCompound tag) => tag.Add(nameof(profanity), profanity);
    public override void LoadData(TagCompound tag) => profanity = tag.GetFloat(nameof(profanity));

    public override void Update()
    {
        if (!IsTileValidForEntity(Position.X, Position.Y))
            ModContent.GetInstance<RelicDesecrationTE>().Kill(Position.X, Position.Y);
    }
}

internal class RelicTile : GlobalTile
{
    internal static HashSet<int> RelicTypes = null;

    private static Asset<Texture2D> SkullTex = null;
    private static int SkullNumber = 0;
    private static int TotalSkulls = 0;

    public override void Load()
    {
        RelicTypes = [TileID.MasterTrophyBase];
        SkullTex = ModContent.Request<Texture2D>("PeculiarJewelry/Assets/Textures/ProfanitySkulls");
    }

    public override void Unload()
    {
        RelicTypes = null;
        SkullTex = null;
    }

    internal static void Add(int type)
    {
        if (!RelicTypes.Contains(type))
            RelicTypes.Add(type);
    }

    public static bool IsRelic(ref int i, ref int j)
    {
        Tile tile = Main.tile[i, j];
        i -= tile.TileFrameX % 54 / 18;
        j -= tile.TileFrameY % 72 / 18;
        return RelicTypes.Contains(tile.TileType);
    }

    public override void PlaceInWorld(int i, int j, int type, Item item)
    {
        if (item.createTile == -1 || !RelicTypes.Contains(item.createTile))
            return;

        if (IsRelic(ref i, ref j))
        {
            ModContent.GetInstance<RelicDesecrationTE>().Place(i, j);
            float profanity = item.GetGlobalItem<RelicDesecrationItem>().profanity;
            (TileEntity.ByPosition[new(i, j)] as RelicDesecrationTE).profanity = profanity;
        }
    }

    public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
    {
        if (IsRelic(ref i, ref j) && TileEntity.ByPosition.TryGetValue(new(i, j), out var te) && te is RelicDesecrationTE)
            ModContent.GetInstance<RelicDesecrationTE>().Kill(i, j);
    }

    public override void PostDraw(int i, int j, int type, SpriteBatch spriteBatch)
    {
        if (!IsRelic(ref i, ref j))
            return;

        if (!TileEntity.ByPosition.TryGetValue(new(i, j), out var te) || te is not RelicDesecrationTE relic)
            return;

        if (relic.profanity == 0)
            return;

        DecayProfanity(relic.profanity, out int massiveCount, out int mediumCount, out int normalCount);
        Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
        Vector2 basePosition = new Vector2(i + 1, j + 1).ToWorldCoordinates() - Main.screenPosition + zero;

        SkullNumber = 0;
        TotalSkulls = massiveCount + mediumCount + normalCount;

        void DrawSkull(Rectangle source)
        {
            float rot = SkullNumber / (float)TotalSkulls * MathF.Tau;
            Vector2 offset = new Vector2(42, 0).RotatedBy(rot + Main.GameUpdateCount / 300f);
            Color col = Lighting.GetColor((basePosition + offset + Main.screenPosition).ToTileCoordinates());
            spriteBatch.Draw(SkullTex.Value, (basePosition + offset).Floor(), source, col, 0f, source.Size() / 2f, 1f, SpriteEffects.None, 0);

            Rectangle glowSource = new(source.X, 46, source.Width, source.Height);
            spriteBatch.Draw(SkullTex.Value, (basePosition + offset).Floor(), glowSource, Color.White, 0f, source.Size() / 2f, 1f, SpriteEffects.None, 0);
            SkullNumber++;
        }

        for (int m = 0; m < massiveCount; ++m)
            DrawSkull(new(56, 0, 44, 44));

        for (int m = 0; m < mediumCount; ++m)
            DrawSkull(new(22, 0, 32, 34));

        for (int m = 0; m < normalCount; ++m)
            DrawSkull(new(0, 0, 20, 24));
    }

    private static void DecayProfanity(float profanity, out int massiveCount, out int mediumCount, out int normalCount)
    {
        massiveCount = 0;

        while (profanity >= 25)
        {
            massiveCount++;
            profanity -= 25;
        }

        mediumCount = 0;

        while (profanity >= 5)
        {
            mediumCount++;
            profanity -= 5;
        }

        normalCount = (int)profanity;
    }
}