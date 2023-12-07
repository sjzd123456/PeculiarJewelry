using PeculiarJewelry.Content.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;
using System;
using System.Collections.Generic;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace PeculiarJewelry.Content.JewelryMechanic.UI.Superimposition;

internal class JewelTriggerUI : UIElement
{
    public bool Showing { get; private set; }

    private readonly Action<TriggerEffect, bool> _onClick;

    private UIButton<string> _context;
    private UIButton<string> _effect;

    private bool _triggerClicked = false;

    public JewelTriggerUI(Action<TriggerEffect, bool> onClick)
    {
        _onClick = onClick;
        Hide(true);
    }

    internal void RebuildStats(Jewel jewel)
    {
        Hide();

        var effect = (jewel.info as MajorJewelInfo).effect;

        _context = new(Language.GetText("Mods.PeculiarJewelry.Jewelry.TriggerContexts." + effect.Context).Value)
        {
            Width = StyleDimension.FromPercent(1f),
            Height = StyleDimension.FromPercent(0.5f),
        };
        _context.OnLeftClick += (_, _) => _onClick.Invoke(effect, true);
        _context.UseAltColors = () => true;
        Append(_context);

        _effect = new(Language.GetText("Mods.PeculiarJewelry.Jewelry.TriggerEffects." + effect.GetType().Name).WithFormatArgs("x").Value)
        {
            Width = StyleDimension.FromPercent(1f),
            Height = StyleDimension.FromPercent(0.5f),
            Top = StyleDimension.FromPercent(0.5f)
        };
        _effect.OnLeftClick += (_, _) => _onClick.Invoke(effect, false);
        _effect.UseAltColors = () => true;
        Append(_effect);

        Showing = true;
    }

    internal void Highlight(Color highlight, Color clear, bool isContext)
    {
        if (_context is not null)
            _context.AltPanelColor = _context.AltHoverPanelColor = isContext ? highlight : clear;

        if (_effect is not null)
            _effect.AltPanelColor = _effect.AltHoverPanelColor = !isContext ? highlight : clear;
    }

    internal void Hide(bool showNoJewel = false)
    {
        RemoveAllChildren();
        Showing = false;

        if (showNoJewel)
            Append(new UIImage(ModContent.Request<Texture2D>("PeculiarJewelry/Content/JewelryMechanic/UI/Superimposition/MissingJewel"))
            {
                HAlign = 0.5f,
                VAlign = 0.5f
            });
    }
}
