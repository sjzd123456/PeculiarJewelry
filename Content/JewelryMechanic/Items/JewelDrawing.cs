using PeculiarJewelry.Content.JewelryMechanic.Stats;
using ReLogic.Content;
using System;

namespace PeculiarJewelry.Content.JewelryMechanic.Items;

public static class JewelDrawing
{
    public static void DrawJewel(Asset<Texture2D> tex, Vector2 position, Vector2 origin, Color color, float rotation, float scale, int frameWidth, int frameHeight, JewelInfo info)
    {
        float cutAmount = MathF.Floor(info.successfulCuts / (float)info.MaxCuts / 5f * 25);

        if (cutAmount == 5)
            cutAmount--;

        var frame = new Rectangle(0, (int)(frameHeight * cutAmount), frameWidth, frameHeight);
        Main.spriteBatch.Draw(tex.Value, position, frame, color, rotation, origin, scale, SpriteEffects.None, 0);
    }
}
