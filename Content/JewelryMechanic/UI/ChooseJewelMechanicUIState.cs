﻿using PeculiarJewelry.Content.JewelryMechanic.UI;
using PeculiarJewelry.Content.JewelryMechanic.UI.Superimposition;
using ReLogic.Content;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace PeculiarJewelry.Content.JewelryMechanic.UI;

internal class ChooseJewelMechanicUIState : UIState
{
    private static Asset<Texture2D> _CutTexture;
    private static Asset<Texture2D> _SetTexture;
    private static Asset<Texture2D> _SuperimpositionTexture;

    private NPC _LapidaristOwner => Main.npc[_lapidaristWhoAmI];

    private int _lapidaristWhoAmI = 0;

    static ChooseJewelMechanicUIState()
    {
        _CutTexture = ModContent.Request<Texture2D>("PeculiarJewelry/Content/JewelryMechanic/UI/JewelCut");
        _SetTexture = ModContent.Request<Texture2D>("PeculiarJewelry/Content/JewelryMechanic/UI/JewelSet");
        _SuperimpositionTexture = ModContent.Request<Texture2D>("PeculiarJewelry/Content/JewelryMechanic/UI/Superimposition");
    }

    public ChooseJewelMechanicUIState(int whoAmI)
    {
        _lapidaristWhoAmI = whoAmI;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        //if (_LapidaristOwner.DistanceSQ(Main.LocalPlayer.Center) > 400 * 400)
            //JewelUISystem.SwitchUI(null);
    }

    public override void OnInitialize()
    {
        UIPanel panel = new() // Main back panel
        {
            Width = StyleDimension.FromPixels(310),
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

            JewelUISystem.SwitchUI(new CutJewelUIState());
        };
        panel.Append(cutButton);

        UIText cutJewelsText = new("Cut Jewels", 0.8f)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(-10)
        };
        cutButton.Append(cutJewelsText);

        UIImageButton setButton = new(_SetTexture)
        {
            Width = StyleDimension.FromPixels(32),
            Height = StyleDimension.FromPixels(32),
            Left = StyleDimension.FromPixels(120),
            Top = StyleDimension.FromPixelsAndPercent(-30, 1),
            VAlign = 1f,
        };
        setButton.OnLeftClick += (UIMouseEvent evt, UIElement listeningElement) =>
        {
            Main.npcChatText = "You ready to set your jewels? Be sure of your choice, lest you waste some precious stones.";
            Main.playerInventory = true;

            JewelUISystem.SwitchUI(new SetJewelUIState());
        };
        panel.Append(setButton);

        UIText setText = new("Set Jewels", 0.8f)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(-10)
        };
        setButton.Append(setText);

        UIImageButton impositionButton = new(_SuperimpositionTexture)
        {
            Width = StyleDimension.FromPixels(32),
            Height = StyleDimension.FromPixels(32),
            Left = StyleDimension.FromPixels(220),
            Top = StyleDimension.FromPixelsAndPercent(-30, 1),
            VAlign = 1f,
        };
        impositionButton.OnLeftClick += (UIMouseEvent evt, UIElement listeningElement) =>
        {
            Main.npcChatText = "Superimposition...fun. Choose well!";
            Main.playerInventory = true;

            JewelUISystem.SwitchUI(new SuperimpositionUIState());
        };
        panel.Append(impositionButton);

        UIText impositionText = new("Superimposition", 0.8f)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(-10)
        };
        impositionButton.Append(impositionText);
    }
}