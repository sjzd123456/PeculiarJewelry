using PeculiarJewelry.Content.JewelryMechanic.Stats;
using ReLogic.Content;

namespace PeculiarJewelry.Content.JewelryMechanic.Items;

public static class JewelDrawing
{
    public static void DrawJewel(Asset<Texture2D> tex, Vector2 position, Vector2 origin, Color color, float rotation, float scale, int frameWidth, int frameHeight, JewelInfo info)
    {
        float cutAmount = (info.cuts / (float)info.MaxCuts) / 5f * 5;
        var frame = new Rectangle(0, frameHeight * (int)cutAmount, frameWidth, frameHeight);
        Main.spriteBatch.Draw(tex.Value, position, frame, color, rotation, origin, scale, SpriteEffects.None, 0);
    }
}
