using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace PeculiarJewelry.Content.JewelryMechanic.UI.Superimposition;

internal class JewelSubstatUI : UIElement
{
    public bool Showing { get; private set; }

    private readonly Action<JewelStat> _onClick;
    private Dictionary<JewelStat, UIButton<string>> _buttonsByStat = new();
    private Dictionary<UIButton<string>, bool> _stupidIsClicked = new();

    public JewelSubstatUI(Action<JewelStat> onClick)
    {
        _onClick = onClick;
        Hide(true);
    }

    public JewelSubstatUI(Jewel jewel, Action<JewelStat> onClick, bool show = false)
    {
        _onClick = onClick;

        if (show)
            RebuildStats(jewel);
    }

    internal void RebuildStats(Jewel jewel)
    {
        Hide();

        _buttonsByStat.Clear();

        for (int i = 0; i < jewel.info.SubStats.Count; i++)
        {
            JewelStat sub = jewel.info.SubStats[i];

            UIButton<string> subButton = new(sub.GetName().Value)
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1 / (float)jewel.info.SubStats.Count),
                Top = StyleDimension.FromPercent((float)i / jewel.info.SubStats.Count),
            };
            subButton.OnLeftClick += (_, _) => _onClick.Invoke(sub);
            subButton.UseAltColors = () => true;
            Append(subButton);

            _buttonsByStat.Add(sub, subButton);
            _stupidIsClicked.Add(subButton, false);
        }

        Showing = true;
    }

    internal void Highlight(Color highlight, Color clear, List<JewelStat> stats)
    {
        foreach (var pair in _buttonsByStat)
        {
            if (stats.Contains(pair.Key))
                pair.Value.AltPanelColor = pair.Value.AltHoverPanelColor = highlight;
            else
                pair.Value.AltPanelColor = pair.Value.AltHoverPanelColor = clear;

            _stupidIsClicked[pair.Value] = stats.Contains(pair.Key);
            pair.Value.OnActivate();
        }
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
