using PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses;
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
