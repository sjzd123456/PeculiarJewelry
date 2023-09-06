using PeculiarJewelry.Content.JewelryMechanic.Stats;
using ReLogic.Content;

namespace PeculiarJewelry.Content.JewelryMechanic.Items;

public static class JewelDrawing
{
    public static void DrawJewel(Asset<Texture2D> tex, Vector2 position, Vector2 origin, Color color, float rotation, float scale, int frameWidth, int frameHeight, JewelInfo info)
    {
        var frame = new Rectangle(0, frameHeight * info.GetDisplayTier(), frameWidth, frameHeight);
        Main.spriteBatch.Draw(tex.Value, position, frame, color, rotation, origin, scale, SpriteEffects.None, 0);
    }
}
