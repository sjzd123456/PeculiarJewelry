using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace PeculiarJewelry.Content.JewelryMechanic.UI.Superimposition;

internal class JewelTriggerUI : UIElement
{
    public bool Showing { get; private set; }

    private readonly Action<JewelStat> _onClick;

    public JewelTriggerUI(Action<JewelStat> onClick)
    {
        _onClick = onClick;
    }

    public JewelTriggerUI(Jewel jewel, Action<JewelStat> onClick, bool show = false)
    {
        _onClick = onClick;

        if (show)
            RebuildStats(jewel);
    }

    internal void RebuildStats(Jewel jewel)
    {
        Hide();

        for (int i = 0; i < jewel.info.SubStats.Count; i++)
        {
            JewelStat sub = jewel.info.SubStats[i];

            UIButton<string> subButton = new(sub.GetName().Value)
            {
                Width = StyleDimension.FromPercent(1f),
                Height = StyleDimension.FromPercent(1 / (float)jewel.info.SubStats.Count),
                Top = StyleDimension.FromPercent((float)i / jewel.info.SubStats.Count)
            };
            subButton.OnLeftClick += (_, _) => _onClick.Invoke(sub);
            Append(subButton);
        }

        Showing = true;
    }

    internal void Hide()
    {
        RemoveAllChildren();
        Showing = false;
    }
}
