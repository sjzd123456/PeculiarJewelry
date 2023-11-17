using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace PeculiarJewelry.Content.JewelryMechanic.UI.Superimposition;

internal class SuperimpositionUIState : UIState, IClosableUIState
{
    private ItemSlotUI _leftJewel;
    private ItemSlotUI _rightJewel;
    private JewelSubstatUI _leftStats;
    private JewelSubstatUI _rightStats;

    private List<JewelStat> _storedStats = new List<JewelStat>();

    internal static string Localize(string postfix) => Language.GetTextValue("Mods.PeculiarJewelry.UI.SuperimpositionMenu." + postfix);

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_leftJewel.Item.ModItem is Jewel jewel && !_leftStats.Showing)
            _leftStats.RebuildStats(jewel);

        if (_leftJewel.Item.ModItem is not Jewel && _leftStats.Showing)
            _leftStats.Hide();

        if (_rightJewel.Item.ModItem is Jewel rightJewel && !_rightStats.Showing)
            _rightStats.RebuildStats(rightJewel);

        if (_rightJewel.Item.ModItem is not Jewel && _rightStats.Showing)
            _rightStats.Hide();
    }

    public override void OnInitialize()
    {
        SetDialoguePanel();
        SetSuperimpositionPanel();
    }

    private void SetSuperimpositionPanel()
    {
        UIPanel panel = new() // Main back panel
        {
            Width = StyleDimension.FromPixels(300),
            Height = StyleDimension.FromPixels(340),
            Top = StyleDimension.FromPixels(60),
            HAlign = 0.5f,
        };
        Append(panel);

        panel.Append(new UIText("Superimposition")
        {
            HAlign = 0.5f,
        });

        Item air = new();
        air.TurnToAir();
        _leftJewel = new ItemSlotUI(new Item[] { air }, 0, ItemSlot.Context.ChestItem, (item, ui) => CanJewelSlotAcceptItem(item, true))
        {
            Left = StyleDimension.FromPixels(10)
        };
        panel.Append(_leftJewel);

        _rightJewel = new ItemSlotUI(new Item[] { air }, 0, ItemSlot.Context.ChestItem, (item, ui) => CanJewelSlotAcceptItem(item, false))
        {
            Left = StyleDimension.FromPixelsAndPercent(-54, 1)
        };
        panel.Append(_rightJewel);

        panel.Append(new UIText("Stats")
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(40)
        });

        _leftStats = new JewelSubstatUI(TryAddStat)
        {
            Width = StyleDimension.FromPixels(134),
            Height = StyleDimension.FromPercent(0.4f),
            Top = StyleDimension.FromPixels(70)
        };
        panel.Append(_leftStats);

        _rightStats = new JewelSubstatUI(TryAddStat)
        {
            Width = StyleDimension.FromPixels(134),
            Height = StyleDimension.FromPercent(0.4f),
            Top = StyleDimension.FromPixels(70),
            HAlign = 1f,
        };
        panel.Append(_rightStats);
    }

    private void TryAddStat(JewelStat stat)
    {
        if (_storedStats.Count <= 2)
            _storedStats.Add(stat);

        _leftStats.Highlight(UICommon.DefaultUIBlueMouseOver, UICommon.DefaultUIBlue, _storedStats);
        _rightStats.Highlight(UICommon.DefaultUIBlueMouseOver, UICommon.DefaultUIBlue, _storedStats);
    }

    private bool CanJewelSlotAcceptItem(Item item, bool isLeft)
    {
        bool isMouseItem = Main.LocalPlayer.selectedItem == 58 && Main.LocalPlayer.HeldItem == item;
        bool isCompatible = true;
        string otherName = string.Empty;

        if (isLeft && _rightJewel.HasItem)
            otherName = GetExistingJewelName(_rightJewel.Item.ModItem as Jewel);
        else if (!isLeft && _leftJewel.HasItem)
            otherName = GetExistingJewelName(_leftJewel.Item.ModItem as Jewel);

        if (otherName != string.Empty && item.ModItem is Jewel jewelForName)
        {
            string name = GetExistingJewelName(jewelForName);
            isCompatible = name == otherName;
        }

        return isCompatible && ((item.ModItem is Jewel jewel && jewel.info.SubStats.Any()) || item.IsAir || !isMouseItem);
    }

    private static string GetExistingJewelName(Jewel jewel)
    {
        List<TooltipLine> lines = new() { new TooltipLine(ModLoader.GetMod("PeculiarJewelry"), "ItemName", "") };
        Jewel.PlainJewelTooltips(lines, jewel.info, jewel, true);
        string jewelName = lines.First(x => x.Name == "ItemName").Text;
        return jewelName;
    }

    private void SetDialoguePanel()
    {
        Append(new UINPCDialoguePanel()
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(204),
            Width = StyleDimension.FromPixels(300),
            Height = StyleDimension.FromPixels(600)
        });
    }

    public void Close()
    {

    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        float oldScale = Main.inventoryScale;
        Main.inventoryScale = 0.9f;
        base.Draw(spriteBatch);
        Main.inventoryScale = oldScale;
    }
}
