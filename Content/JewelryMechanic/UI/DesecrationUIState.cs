using PeculiarJewelry.Content.JewelryMechanic.Desecration;
using System;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace PeculiarJewelry.Content.JewelryMechanic.UI;

internal class DesecrationUIState : UIState, IClosableUIState
{
    private readonly Dictionary<string, float> TemporaryStrength = [];

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void OnInitialize()
    {
        UIPanel panel = new()
        {
            Width = StyleDimension.FromPixels(500),
            Height = StyleDimension.FromPixels(400),
            HAlign = 0.5f,
            VAlign = 0.15f
        };

        Append(panel);

        UINPCDialoguePanel dialoguePanel = new(500)
        {
            HAlign = 0.5f,
            VAlign = 0.15f,
            Top = StyleDimension.FromPixels(180)
        };

        Append(dialoguePanel);
        BuildList(panel);

        UIButton<string> confirm = new("Confirm")
        {
            Width = StyleDimension.FromPixels(80),
            Height = StyleDimension.FromPixels(32),
            VAlign = 1,
        };
        confirm.OnLeftClick += ConfirmClick;
        panel.Append(confirm);
    }

    private void ConfirmClick(UIMouseEvent evt, UIElement listeningElement)
    {
        foreach (var (key, value) in TemporaryStrength)
            ModContent.GetInstance<DesecratedSystem>().SetDesecration(key, value);

        TemporaryStrength.Clear();
    }

    private void BuildList(UIPanel panel)
    {
        UIList desecrationsList = new()
        {
            Width = StyleDimension.Fill,
            Height = StyleDimension.FromPixelsAndPercent(-40, 1f),
        };

        UIScrollbar bar = new()
        {
            Width = StyleDimension.FromPixels(20),
            Height = StyleDimension.FromPixelsAndPercent(-40, 1f),
            Left = StyleDimension.FromPixelsAndPercent(-20, 1)
        };

        desecrationsList.SetScrollbar(bar);
        panel.Append(bar);
        panel.Append(desecrationsList);

        PopulateList(desecrationsList);
    }

    private void PopulateList(UIList desecrationsList)
    {
        foreach (var (key, value) in DesecrationModifier.Desecrations)
        {
            UIPanel singleItemPanel = new()
            {
                Width = StyleDimension.FromPixelsAndPercent(-24, 1f),
                Height = StyleDimension.FromPixels(50),
            };

            float size = FontAssets.MouseText.Value.MeasureString(value.DesecrationName).X;
            UIText text = new(value.DesecrationName)
            {
                Width = StyleDimension.FromPixels(size),
                VAlign = 0.5f,
            };
            singleItemPanel.Append(text);

            UIButton<string> increaseButton = new("+")
            {
                Width = StyleDimension.FromPixels(40),
                Height = StyleDimension.FromPixels(50),
                Left = StyleDimension.FromPixels(size + 16),
                VAlign = 0.5f,
            };
            increaseButton.OnLeftClick += (sender, e) => ClickIncrease(key);
            singleItemPanel.Append(increaseButton);

            UIText countToMax = new("0/" + (value.StrengthCap != -1 ? value.StrengthCap : "∞"))
            {
                HAlign = 1f,
                VAlign = 0.5f,
            };

            countToMax.OnUpdate += (self) => ModifyCountText(self as UIText, key);
            singleItemPanel.Append(countToMax);
            desecrationsList.Add(singleItemPanel);
        }
    }

    private void ClickIncrease(string key)
    {
        if (!TemporaryStrength.ContainsKey(key))
            TemporaryStrength.Add(key, DesecrationModifier.Desecrations[key].strength);

        float cap = DesecrationModifier.Desecrations[key].StrengthCap;

        if (cap != -1)
            TemporaryStrength[key] = MathHelper.Clamp(TemporaryStrength[key] + 1, 0, cap);
        else
            TemporaryStrength[key]++;
    }

    private void ModifyCountText(UIText uiText, string key)
    {
        var dese = DesecrationModifier.Desecrations[key];
        bool hasTemp = TemporaryStrength.TryGetValue(key, out float strength);
        var str = hasTemp ? strength : dese.strength;

        uiText.SetText(str + "/" + (dese.StrengthCap != -1 ? dese.StrengthCap : "∞"));
        uiText.TextColor = hasTemp ? Color.Red : Color.White;
    }

    public void Close()
    {
    }
}
