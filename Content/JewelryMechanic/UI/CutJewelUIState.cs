using PeculiarJewelry.Content.JewelryMechanic.Items;
using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Items.JewelSupport;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

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
    private UIText _infoText = null;
    private string _dustPriceText = null;
    private string _chanceText = null;
    private string _statusText = string.Empty;

    public override void Update(GameTime gameTime)
    {
        _storedItem = _cutSlot.Item;
        _hasJewel = _storedItem.ModItem is Jewel;
        ResetStatPanelWith(_storedItem);
        UpdateInfo();
    }

    private void UpdateInfo(string status = null)
    {
        static string HighlightStat(int value, int cutoff)
        {
            string white = "FFFFFF";
            string red = "FF0000";
            return $"[c/{(value >= cutoff ? white : red)}:{value}]";
        }

        string noJewel = "(No jewel selected)";
        float price = _hasJewel ? JewelCutCoinPrice(JewelItem.info) : 0;
        int goldCoins = (int)(Utils.CoinsCount(out bool _, Main.LocalPlayer.inventory) / Item.buyPrice(0, 1, 0, 0));
        int endCoins = Utils.CoinsSplit((long)price)[2];
        string coinPrice = !_hasJewel ? noJewel : HighlightStat(goldCoins, endCoins) + "/" + endCoins + $" gold [i:{ItemID.GoldCoin}]";

        int dustPriceNum = _hasJewel ? JewelCutDustPrice(JewelItem.info) : 0;
        string dustPriceText = HighlightStat(Main.LocalPlayer.CountItem(ModContent.ItemType<SparklyDust>()), dustPriceNum);
        string dustPrice = !_hasJewel ? "" : dustPriceText + "/" + dustPriceNum.ToString() + $" dust [i:{ModContent.ItemType<SparklyDust>()}]";

        float chance = _hasJewel ? JewelCutChance(JewelItem.info, _supportItems) : -1;
        float chanceAmount = Math.Min(chance * 100, 100);

        bool thresholdCut = _hasJewel && (JewelItem.info.cuts - 7) % 9 == 0;
        string echoCost = "";

        if (thresholdCut)
        {
            int itemID = JewelItem.info.cuts switch 
            {
                7 => ModContent.ItemType<ResonantEcho>(),
                _ => throw new ArgumentException("Uh oh! You aren't in a threshold but you are somehow."),
            };
            echoCost = $"0/2 [i:{itemID}] ({Lang.GetItemName(itemID)})";
        }

        string hex;

        if (chanceAmount > 80)
            hex = "00FF00";
        else if (chanceAmount > 40)
            hex = "FFFF00";
        else if (chanceAmount > 0)
            hex = "FF0000";
        else
            hex = "000000";

        string cutChance = chance == -1 ? "" : $"[c/{hex}: {chanceAmount:#0.##}]% success chance";

        if (!_hasJewel)
            _statusText = string.Empty;
        else if (status is not null)
            _statusText = status;

        _infoText.SetText(coinPrice + "\n" + dustPrice + "\n" + cutChance + "\n" + echoCost + "\n" + _statusText);
    }

    private static int JewelCutCoinPrice(JewelInfo info) => Item.buyPrice(0, 1) * ((int)info.tier + info.successfulCuts + 1);
    private static int JewelCutDustPrice(JewelInfo info) => ((int)info.tier + 1) * (info.successfulCuts + 1);

    private static float JewelCutChance(JewelInfo info, ItemSlotUI[] support, bool onlyCheck = true)
    {
        float baseChance = 1f - (info.successfulCuts * 0.05f);
        float baseOverrideChance = -1;
        Dictionary<int, int> itemCountsById = new();

        foreach (var item in support)
        {
            if (itemCountsById.ContainsKey(item.Item.type))
                itemCountsById[item.Item.type]++;
            else
                itemCountsById.Add(item.Item.type, 1);
        }

        foreach (var item in support)
        {
            if (item.Item.ModItem is not JewelSupportItem supportItem)
                continue;

            if (supportItem.HardOverrideJewelCutChance(info, out float newChance))
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

            baseChance = (item.Item.ModItem as JewelSupportItem).ModifyJewelCutChance(info, baseChance);
        }

        if (!onlyCheck)
            ClearSupportItems(support);

        return baseChance;
    }

    private static void ClearSupportItems(ItemSlotUI[] support)
    {
        foreach (var item in support)
        {
            item.Item.stack--;

            if (item.Item.stack <= 0)
                item.Item.TurnToAir();
        }
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
        _statPanel.Height = StyleDimension.FromPixels(Math.Max(_statPanel.Children.Count(), 2) * 22 + 6);
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

        static string Replacement(string text, out bool invalidText)
        {
            invalidText = false;

            int plus = text.IndexOf('+') + 1;
            int percent = text.IndexOf('%', plus);
            int space = text.IndexOf(' ', plus);
            int end = percent;

            if (percent < 0 || space < 0 || Math.Abs(percent - space) <= 0)
            {
                invalidText = true;
                return text;
            }

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

            string replacement = Replacement(text.Text, out bool _);
            float current = info.Major.GetEffectValue(PeculiarJewelry.StatConfig.GlobalPowerScaleMinimum);
            return text.Text.Replace(replacement, "[c/ffa500:" + current.ToString("#0.##") + " - " + info.Major.GetEffectValue(1f).ToString("#0.##") + "]");
        }

        if (text.Name.StartsWith("SubStat"))
        {
            overrideColor = Color.LightBlue;

            int statID = int.Parse(text.Name[^1].ToString());
            string replacement = Replacement(text.Text, out bool invalidText);

            if (invalidText )
                return text.Text;

            float current = info.SubStats[statID].GetEffectValue(PeculiarJewelry.StatConfig.GlobalPowerScaleMinimum);
            return text.Text.Replace(replacement, "[c/ffa500:" + current.ToString("#0.##") + " - " + info.SubStats[statID].GetEffectValue(1f).ToString("#0.##") + "]");
        }
        return text.Text;
    }

    private void SetPriceInfoPanel()
    {
        UIPanel infoPanel = new() // Main back panel
        {
            Width = StyleDimension.FromPixels(240),
            Height = StyleDimension.FromPixels(CutPanelHeight + 30),
            Left = StyleDimension.FromPixelsAndPercent(-404, 0.5f),
            Top = StyleDimension.FromPercent(0.25f),
        };
        Append(infoPanel);

        _infoText = new(string.Empty)
        {
            IsWrapped = false,
            Width = StyleDimension.Fill
        };
        infoPanel.Append(_infoText);
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
        if (JewelItem is null)
            return false;

        if (item.ModItem is not JewelSupportItem && !item.IsAir && item == Main.mouseItem)
            return false;

        foreach (var slot in _supportItems)
            if (!slot.Item.IsAir && item.type == slot.Item.type)
                return false;
        
        return item.ModItem is not JewelSupportItem support || support.CanBePlacedInSupportSlot(JewelItem.info);
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
        {
            UpdateInfo("[c/ff0000:Missing] jewel!");
            return; // No jewel stored
        }

        JewelInfo info = (_storedItem.ModItem as Jewel).info;

        if (info.cuts >= info.MaxCuts)
        {
            UpdateInfo("[c/ff0000:Jewel cannot be cut] any\nfurther!");
            return; // Too many cuts
        }

        int coinPrice = JewelCutCoinPrice(info);
        if (Utils.CoinsCount(out bool overflow, Main.LocalPlayer.inventory) < coinPrice || overflow)
        {
            UpdateInfo("Not enough [c/ff0000:coins]!");
            return;
        }

        int dustPrice = JewelCutDustPrice(info);
        if (Main.LocalPlayer.CountItem(ModContent.ItemType<SparklyDust>()) < dustPrice)
        {
            UpdateInfo($"Not enough [i:{ModContent.ItemType<SparklyDust>()}] [c/ff0000:Dust]!");
            return;
        }

        for (int i = 0; i < dustPrice; ++i)
            Main.LocalPlayer.ConsumeItem(ModContent.ItemType<SparklyDust>(), true);

        Main.LocalPlayer.BuyItem(coinPrice);
        SoundEngine.PlaySound(SoundID.NPCHit4, Main.LocalPlayer.Center);
        bool success = info.TryAddCut(JewelCutChance(info, _supportItems, false));

        if (success)
            UpdateInfo("Cut [c/00FF00:successful!]");
        else
            UpdateInfo("Cut [c/FF0000:failed.]");
    }
}
