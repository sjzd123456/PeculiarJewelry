using ReLogic.Graphics;
using ReLogic.Text;
using System.Threading;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.UI.Chat;

namespace PeculiarJewelry.Content.JewelryMechanic.UI;

internal class UINPCDialoguePanel : UIElement
{
    DynamicSpriteFont Font => FontAssets.MouseText.Value;

    UIPanel _panel;
    UIText _text;
    float _width;

    public UINPCDialoguePanel(float width = 200)
    {
        _width = width;
    }

    public override void OnInitialize()
    {
        Width = StyleDimension.FromPixels(_width);
        Height = StyleDimension.FromPixels(40);

        _panel = new()
        {
            Width = StyleDimension.FromPixels(_width),
            Height = StyleDimension.Fill,
            HAlign = 0.5f,
            Top = Top
        };
        Append(_panel);

        _text = new UIText(Main.npcChatText)
        {
            HAlign = 0.5f,
            Width = StyleDimension.FromPixels(_width),
            Height = StyleDimension.Fill,
            IsWrapped = true,
        };
        _panel.Append(_text);
    }

    public override void Update(GameTime gameTime)
    {
        if (Main.npcChatText != "")
            _text.SetText(Main.npcChatText);

        var size = ChatManager.GetStringSize(Font, Font.CreateWrappedText(_text.Text, _panel.GetInnerDimensions().Width), Vector2.One);
        size = !_text.IsWrapped ? new Vector2(size.X, 16f) : new Vector2(size.X, size.Y + _text.WrappedTextBottomPadding);

        Height = StyleDimension.FromPixels(size.Y);
        //_panel.Height = StyleDimension.FromPixels(size.Y);
    }
}
