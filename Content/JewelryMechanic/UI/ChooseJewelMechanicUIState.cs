using PeculiarJewelry.Content.JewelryMechanic.UI;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FF6Mod.UI.Betting;

internal class ChooseJewelMechanicUIState : UIState
{
    private static Asset<Texture2D> _CutTexture;
    private static Asset<Texture2D> _SuperimpositionTexture;

    private NPC _LapidaristOwner => Main.npc[_lapidaristWhoAmI];

    private int _lapidaristWhoAmI = 0;

    static ChooseJewelMechanicUIState()
    {
        _CutTexture = ModContent.Request<Texture2D>("PeculiarJewelry/Content/JewelryMechanic/UI/JewelCut");
        _SuperimpositionTexture = ModContent.Request<Texture2D>("PeculiarJewelry/Content/JewelryMechanic/UI/Superimposition");
    }

    public ChooseJewelMechanicUIState(int whoAmI)
    {
        _lapidaristWhoAmI = whoAmI;
    }

    public override void Update(GameTime gameTime)
    {
        if (_LapidaristOwner.DistanceSQ(Main.LocalPlayer.Center) > 400 * 400)
            SwitchUI(null);
    }

    public override void OnInitialize()
    {
        UIPanel panel = new() // Main back panel
        {
            Width = StyleDimension.FromPixels(300),
            Height = StyleDimension.FromPixels(60),
            HAlign = 0.5f,
            VAlign = 0.25f
        };
        Append(panel);

        UIImageButton cutButton = new(_CutTexture)
        {
            Width = StyleDimension.FromPixels(32),
            Height = StyleDimension.FromPixels(32),
            Left = StyleDimension.FromPixels(20),
            Top = StyleDimension.FromPixelsAndPercent(-30, 1),
            VAlign = 1f,
        };

        cutButton.OnLeftClick += (UIMouseEvent evt, UIElement listeningElement) =>
        {
            Main.npcChatText = "You'd like a jewel cut? Give me the jewel and I can do it...for a nominal price.";
            Main.playerInventory = true;

            SwitchUI(new CutJewelUIState());
        };
        panel.Append(cutButton);

        UIText cutJewelsText = new("Cut Jewels", 0.8f)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(-10)
        };
        cutButton.Append(cutJewelsText);

        UIImageButton impositionButton = new(_SuperimpositionTexture)
        {
            Width = StyleDimension.FromPixels(32),
            Height = StyleDimension.FromPixels(32),
            Left = StyleDimension.FromPixels(120),
            Top = StyleDimension.FromPixelsAndPercent(-30, 1),
            VAlign = 1f,
        };
        impositionButton.OnLeftClick += (UIMouseEvent evt, UIElement listeningElement) => SwitchUI(null);
        panel.Append(impositionButton);

        UIText impositionText = new("Superimposition", 0.8f)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(-10)
        };
        impositionButton.Append(impositionText);
    }

    private static void SwitchUI(UIState state) => JewelUISystem.Instance.JewelInterface.SetState(state);
}