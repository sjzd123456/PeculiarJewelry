using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace PeculiarJewelry.Content.JewelryMechanic.Misc;

internal class JewelHeadLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new BeforeParent(PlayerDrawLayers.HeldItem);
    public override bool IsHeadLayer => true;

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        var player = drawInfo.drawPlayer;

        if (player.GetModPlayer<MaterialPlayer>().HasEquip(EquipType.Head, out var info))
        {
            var color = Lighting.GetColor(player.Center.ToTileCoordinates(), info.Color);
            var effect = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 basePosition = new(-player.bodyFrame.Width / 2 + player.width / 2, player.height - player.bodyFrame.Height + 4);
            Vector2 position = (drawInfo.Position - Main.screenPosition + basePosition).Floor() + player.headPosition;
            var data = new DrawData(info.Texture, position, player.bodyFrame, color, 0f, Vector2.Zero, 1f, effect);
            drawInfo.DrawDataCache.Add(data);
        }
    }
}

internal class JewelFaceLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.FaceAcc);
    public override bool IsHeadLayer => true;

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        var player = drawInfo.drawPlayer;

        if (player.GetModPlayer<MaterialPlayer>().HasEquip(EquipType.Face, out var info))
        {
            var color = Lighting.GetColor(player.Center.ToTileCoordinates(), info.Color);
            var effect = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 basePosition = new(-player.bodyFrame.Width / 2 + player.width / 2, player.height - player.bodyFrame.Height + 4);
            Vector2 position = (drawInfo.Position - Main.screenPosition + basePosition).Floor() + player.headPosition;
            var data = new DrawData(info.Texture, position, player.bodyFrame, color, 0f, Vector2.Zero, 1f, effect);
            drawInfo.DrawDataCache.Add(data);
        }
    }
}

internal class JewelNeckLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.NeckAcc);

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        var player = drawInfo.drawPlayer;

        if (player.GetModPlayer<MaterialPlayer>().HasEquip(EquipType.Neck, out var info))
        {
            var color = Lighting.GetColor(player.Center.ToTileCoordinates(), info.Color);
            var effect = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 basePosition = new(-player.bodyFrame.Width / 2 + player.width / 2, player.height - player.bodyFrame.Height + 4);
            Vector2 position = (drawInfo.Position - Main.screenPosition + basePosition).Floor() + player.headPosition;
            var data = new DrawData(info.Texture, position, player.bodyFrame, color, 0f, Vector2.Zero, 1f, effect)
            {
                shader = player.cNeck
            };
            drawInfo.DrawDataCache.Add(data);
        }
    }
}

internal class JewelHandsOnLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.HandOnAcc);

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        var player = drawInfo.drawPlayer;

        if (player.GetModPlayer<MaterialPlayer>().HasEquip(EquipType.HandsOn, out var info))
            DrawHands(drawInfo, player, info);
    }

    private static void DrawHands(PlayerDrawSet drawInfo, Player player, MaterialPlayer.EquipLayerInfo info)
    {
        var color = Lighting.GetColor(player.Center.ToTileCoordinates(), info.Color);
        var effect = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
        Vector2 basePosition = new(-player.bodyFrame.Width / 2 + player.width / 2, player.height - player.bodyFrame.Height + 4);
        Vector2 position = (drawInfo.Position - Main.screenPosition + basePosition).Floor() + player.headPosition;
        var data = new DrawData(info.Texture, position, drawInfo.compFrontArmFrame, color, 0f, Vector2.Zero, 1f, effect)
        {
            shader = player.cHandOn
        };
        drawInfo.DrawDataCache.Add(data);
    }
}

internal class JewelFrontLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.FrontAccFront);

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        var player = drawInfo.drawPlayer;

        if (player.GetModPlayer<MaterialPlayer>().HasEquip(EquipType.Front, out var info))
        {
            var color = Lighting.GetColor(player.Center.ToTileCoordinates(), info.Color);
            var effect = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 basePosition = new(-player.bodyFrame.Width / 2 + player.width / 2, player.height - player.bodyFrame.Height + 4);
            Vector2 position = (drawInfo.Position - Main.screenPosition + basePosition).Floor() + player.headPosition;
            var data = new DrawData(info.Texture, position, player.bodyFrame, color, 0f, Vector2.Zero, 1f, effect)
            {
                shader = player.cFront
            };
            drawInfo.DrawDataCache.Add(data);
        }
    }
}

internal class JewelShoesLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.Shoes);

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        var player = drawInfo.drawPlayer;

        if (player.GetModPlayer<MaterialPlayer>().HasEquip(EquipType.Shoes, out var info))
        {
            var color = Lighting.GetColor(player.Center.ToTileCoordinates(), info.Color);
            var effect = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 basePosition = new(-player.bodyFrame.Width / 2 + player.width / 2, player.height - player.bodyFrame.Height + 4);
            Vector2 position = (drawInfo.Position - Main.screenPosition + basePosition).Floor() + player.headPosition;
            var data = new DrawData(info.Texture, position, player.bodyFrame, color, 0f, Vector2.Zero, 1f, effect)
            {
                shader = player.cShoe
            };
            drawInfo.DrawDataCache.Add(data);
        }
    }
}

internal class JewelWaistLayer : PlayerDrawLayer
{
    public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.WaistAcc);

    protected override void Draw(ref PlayerDrawSet drawInfo)
    {
        var player = drawInfo.drawPlayer;

        if (player.GetModPlayer<MaterialPlayer>().HasEquip(EquipType.Waist, out var info))
        {
            var color = Lighting.GetColor(player.Center.ToTileCoordinates(), info.Color);
            var effect = player.direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            Vector2 basePosition = new(-player.bodyFrame.Width / 2 + player.width / 2, player.height - player.bodyFrame.Height + 4);
            Vector2 position = (drawInfo.Position - Main.screenPosition + basePosition).Floor() + player.headPosition;
            var data = new DrawData(info.Texture, position, player.bodyFrame, color, 0f, Vector2.Zero, 1f, effect)
            {
                shader = player.cWaist
            };
            drawInfo.DrawDataCache.Add(data);
        }
    }
}