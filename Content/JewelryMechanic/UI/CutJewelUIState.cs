using PeculiarJewelry.Content.JewelryMechanic.Items;
using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Items.JewelSupport;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using PeculiarJewelry.Content.JewelryMechanic.Stats.IO;
using ReLogic.Content;
using ReLogic.Graphics;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.UI.Chat;

namespace PeculiarJewelry.Content.JewelryMechanic.UI;

internal class CutJewelUIState : UIState
{
    private const int CutPanelHeight = 180;

    private Jewel JewelItem => _storedItem.ModItem as Jewel;

    private ItemSlotUI _cutSlot = null;
    private ItemSlotUI[] _supportItems = null;
    private Item _storedItem = null;
    private bool _hasJewel = false;
    private UIPanel _statPanel = null;
    private bool _hoveringAnvil = false;

    // Info stuff
    private UIText _priceText = null;
    private UIText _dustPriceText = null;
    private UIText _chanceText = null;

    public override void Update(GameTime gameTime)
    {
        _storedItem = _cutSlot.Item;
        _hasJewel = _storedItem.ModItem is Jewel;
        ResetStatPanelWith(_storedItem);

        string noJewel = "(No jewel selected)";
        float price = _hasJewel ? JewelCutCoinPrice(JewelItem.info) : 0;
        _priceText.SetText(price == 0 ? noJewel : GetPriceText((int)price));

        float dustPrice = _hasJewel ? JewelCutDustPrice(JewelItem.info) : 0;
        _dustPriceText.SetText(price == 0 ? "" : dustPrice.ToString() + $"[i:{ModContent.ItemType<SparklyDust>()}]");

        float chance = _hasJewel ? JewelCutChance(JewelItem.info, _supportItems) : -1;
        _chanceText.SetText(chance == -1 ? "" : Math.Min(chance * 100, 100).ToString("#0.##") + "% success chance");
    }

    private static int JewelCutCoinPrice(JewelInfo info) => Item.buyPrice(0, 1) * ((int)info.tier + info.successfulCuts + 1);
    private static int JewelCutDustPrice(JewelInfo info) => ((int)info.tier + 1) * (info.successfulCuts + 1);

    private static float JewelCutChance(JewelInfo info, ItemSlotUI[] support, bool onlyCheck = true)
    {
        float baseChance = 1f - (info.successfulCuts * 0.05f);
        float baseOverrideChance = -1;

        foreach (var item in support)
        {
            if (item.Item.ModItem is not JewelSupportItem supportItem)
                continue;

            if (supportItem.HardOverrideJewelCutChance(out float newChance))
            {
                if (baseOverrideChance == -1 || newChance > baseOverrideChance)
                    baseOverrideChance = newChance;
            }
        }

        if (baseOverrideChance != -1)
        {
            if (!onlyCheck)
                ClearSupportItems(support);
            return baseOverrideChance;
        }

        foreach (var item in support)
        {
            if (item.Item.ModItem is not JewelSupportItem supportItem)
                continue;

            baseChance = (item.Item.ModItem as JewelSupportItem).ModifyJewelCutChance(baseChance);
        }

        if (!onlyCheck)
            ClearSupportItems(support);
        return baseChance;
    }

    private static void ClearSupportItems(ItemSlotUI[] support)
    {
        foreach (var item in support)
            item.Item.TurnToAir();
    }

    public override void OnInitialize()
    {
        _supportItems = new ItemSlotUI[6];
        SetDialoguePanel();
        SetCutPanel();
        SetPriceInfoPanel();
        SetStatPanel();
    }

    private void SetStatPanel()
    {
        _statPanel = new() // Main back panel
        {
            Width = StyleDimension.FromPixels(360),
            Height = StyleDimension.FromPixels(CutPanelHeight + 80),
            Left = StyleDimension.FromPixelsAndPercent(166, 0.5f),
            Top = StyleDimension.FromPercent(0.25f),
        };
        Append(_statPanel);

        ResetStatPanelWith(_cutSlot.Item);
    }

    private void ResetStatPanelWith(Item item)
    {
        _statPanel.RemoveAllChildren();

        int width = -1;

        if (item.IsAir || item.ModItem is not Jewel)
            _statPanel.Append(new UIText("(No jewel selected)"));
        else
        {
            var subStats = (item.ModItem as Jewel).info.SubStats;
            var tooltips = new List<TooltipLine>() { new TooltipLine(ModLoader.GetMod("PeculiarJewelry"), "ItemName", "Item") };
            item.ModItem.ModifyTooltips(tooltips);

            if (_hoveringAnvil && (item.ModItem as Jewel).info.successfulCuts % 4 == 3)
                tooltips.Add(new TooltipLine(ModLoader.GetMod("PeculiarJewelry"), "SubstatUpgrade", "One substat will be " + (subStats.Count == subStats.Capacity ? "upgraded" : "added")));

            for (int i = 0; i < tooltips.Count; i++)
            {
                var line = tooltips[i];
                string display = TextModification(line, (item.ModItem as Jewel).info, out Color? overrideColor);
                var color = overrideColor ?? Color.White;

                var text = new UIText(display)
                {
                    Top = StyleDimension.FromPixels(20 * i),
                    TextColor = color
                };
                _statPanel.Append(text);

                Vector2 vector = FontAssets.MouseText.Value.MeasureString(display);

                if (width < vector.X)
                    width = (int)vector.X + 20;
            }
        }

        if (width != -1)
            _statPanel.Width = StyleDimension.FromPixels(width);
        _statPanel.Height = StyleDimension.FromPixels(Math.Max(_statPanel.Children.Count(), 2) * 22);
    }

    private string TextModification(TooltipLine text, JewelInfo info, out Color? overrideColor)
    {
        // This whole method is really messy and it sucks and I'm not going to fix it
        // Pay me a ton of money and it'll be better code and otherwise too bad

        overrideColor = null;

        if (info.cuts == info.MaxCuts)
        {
            overrideColor = Color.Gray;
            return text.Text;
        }

        if (!_hoveringAnvil)
            return text.Text;

        static string Replacement(string text)
        {
            int plus = text.IndexOf('+') + 1;
            int percent = text.IndexOf('%', plus);
            int space = text.IndexOf(' ', plus);
            int end = percent;

            if ((space < percent && space != 0) || percent < 0)
                end = space;

            return text[plus..end];
        }

        if (text.Name == "JewelCuts")
        {
            string replacement = text.Text[..(text.Text.IndexOf("/") + 1)];
            return text.Text.Replace(replacement, "[c/ffff00:" + (float.Parse(replacement.Replace("/", "")) - 1).ToString() + "]/");
        }

        if (text.Name == "SubstatUpgrade")
        {
            overrideColor = Color.LightBlue;
            return text.Text;
        }

        if (text.Name.StartsWith("MajorStat"))
        {
            overrideColor = Color.Yellow;

            string replacement = Replacement(text.Text);
            float current = info.Major.GetEffectValue(PeculiarJewelry.StatConfig.GlobalPowerScaleMinimum);
            return text.Text.Replace(replacement, "[c/ffa500:" + current.ToString("#0.##") + " - " + info.Major.GetEffectValue(1f).ToString("#0.##") + "]");
        }

        if (text.Name.StartsWith("SubStat"))
        {
            overrideColor = Color.LightBlue;

            int statID = int.Parse(text.Name[^1].ToString());
            string replacement = Replacement(text.Text);
            float current = info.SubStats[statID].GetEffectValue(PeculiarJewelry.StatConfig.GlobalPowerScaleMinimum);
            return text.Text.Replace(replacement, "[c/ffa500:" + current.ToString("#0.##") + " - " + info.SubStats[statID].GetEffectValue(1f).ToString("#0.##") + "]");
        }
        return text.Text;
    }

    private void SetPriceInfoPanel()
    {
        UIPanel infoPanel = new() // Main back panel
        {
            Width = StyleDimension.FromPixels(200),
            Height = StyleDimension.FromPixels(CutPanelHeight),
            Left = StyleDimension.FromPixelsAndPercent(-364, 0.5f),
            Top = StyleDimension.FromPercent(0.25f),
        };
        Append(infoPanel);

        _priceText = new(string.Empty)
        {
            IsWrapped = true,
            Width = StyleDimension.Fill
        };
        infoPanel.Append(_priceText);

        _dustPriceText = new(string.Empty)
        {
            IsWrapped = true,
            Width = StyleDimension.Fill,
            Top = StyleDimension.FromPixels(26)
        };
        infoPanel.Append(_dustPriceText);

        _chanceText = new(string.Empty)
        {
            IsWrapped = true,
            Width = StyleDimension.Fill,
            Top = StyleDimension.FromPixels(52)
        };
        infoPanel.Append(_chanceText);
    }

    private void SetCutPanel()
    {
        UIPanel panel = new() // Main back panel
        {
            Width = StyleDimension.FromPixels(320),
            Height = StyleDimension.FromPixels(CutPanelHeight),
            Top = StyleDimension.FromPercent(0.25f),
            HAlign = 0.5f,
        };
        Append(panel);

        Item air = new();
        air.TurnToAir();
        _cutSlot = new(new Item[] { air }, 0, ItemSlot.Context.ChestItem, CanJewelSlotAcceptItem)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(14)
        };
        panel.Append(_cutSlot);

        UIText cutText = new("Jewel Cutting")
        {
            HAlign = 0.5f,
            TextColor = Color.Aquamarine,
        };
        panel.Append(cutText);

        UIImageButton button = new(TextureAssets.Item[ItemID.IronAnvil])
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(80),
            Width = StyleDimension.FromPixels(30),
            Height = StyleDimension.FromPixels(30),
        };
        button.OnLeftClick += TryCutJewel;
        button.OnMouseOver += (UIMouseEvent evt, UIElement listeningElement) => _hoveringAnvil = true;
        button.OnMouseOut += (UIMouseEvent evt, UIElement listeningElement) => _hoveringAnvil = false;
        panel.Append(button);

        UIText cutJewelText = new("Cut")
        {
            Top = StyleDimension.FromPixels(-18)
        };
        button.Append(cutJewelText);

        UIText supportText = new("Support")
        {
            Top = StyleDimension.FromPixels(CutPanelHeight - 90),
            Left = StyleDimension.FromPixels(20),
        };
        panel.Append(supportText);

        int max = _supportItems.Length;
        for (int i = 0; i < max; ++i)
        {
            _supportItems[i] = new ItemSlotUI(new Item[] { air }, 0, ItemSlot.Context.ChestItem, CanInputSupportItem)
            {
                HAlign = (float)i / (max - 1),
                VAlign = 0.95f
            };
            panel.Append(_supportItems[i]);
        }
    }

    private static bool CanJewelSlotAcceptItem(Item item, ItemSlotUI _)
    {
        bool isMouseItem = Main.LocalPlayer.selectedItem == 58 && Main.LocalPlayer.HeldItem == item;
        return item.ModItem is Jewel || item.IsAir || !isMouseItem;
    }

    private bool CanInputSupportItem(Item item, ItemSlotUI self)
    {
        if (item.ModItem is not JewelSupportItem && !item.IsAir && item == Main.mouseItem)
            return false;

        foreach (var slot in _supportItems)
            if (!slot.Item.IsAir && item.type == slot.Item.type)
                return false;

        return true;
    }

    private void SetDialoguePanel()
    {
        UIPanel dialoguePanel = new() // Main back panel
        {
            Width = StyleDimension.FromPixels(400),
            Height = StyleDimension.FromPixels(80),
            Top = StyleDimension.FromPixelsAndPercent(-320, 0.25f),
            HAlign = 0.5f,
            VAlign = 0.25f,
        };
        Append(dialoguePanel);

        UIText dialogue = new(Main.npcChatText)
        {
            IsWrapped = true,
            Width = StyleDimension.Fill
        };
        dialogue.OnUpdate += (UIElement self) => (self as UIText).SetText(Main.npcChatText);
        dialoguePanel.Append(dialogue);
    }

    private void TryCutJewel(UIMouseEvent evt, UIElement listeningElement)
    {
        if (!_hasJewel)
            return; // No jewel stored

        JewelInfo info = (_storedItem.ModItem as Jewel).info;

        if (info.cuts >= info.MaxCuts)
            return; // Too many cuts

        SoundEngine.PlaySound(SoundID.NPCHit4, Main.LocalPlayer.Center);
        info.TryAddCut(JewelCutChance(info, _supportItems, false));
    }

    private static string GetPriceText(long rawCost)
    {
        string result = string.Empty;
        int[] coins = Utils.CoinsSplit(rawCost);

        if (coins[3] > 0)
            result += $"{coins[3]} platinum[i:{ItemID.PlatinumCoin}]";

        if (coins[2] > 0)
            result += $"{coins[2]} gold[i:{ItemID.GoldCoin}]";

        if (coins[1] > 0)
            result += $"{coins[1]} silver[i:{ItemID.SilverCoin}]";

        if (coins[0] > 0)
            result += $"{coins[0]} copper[i:{ItemID.CopperCoin}]";

        return result;
    }
}
