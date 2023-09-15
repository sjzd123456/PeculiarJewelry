using PeculiarJewelry.Content.JewelryMechanic.Items;
using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using ReLogic.Content;
using System;
using System.Reflection;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace FF6Mod.UI.Betting;

internal class CutJewelUIState : UIState
{
    private const int CutPanelHeight = 120;

    private static readonly FieldInfo _UIItemSlotItemArray = null;

    private Jewel JewelItem => _storedItem.ModItem as Jewel;

    private UIItemSlot _cutSlot = null;
    private Item _storedItem = null;
    private bool _hasJewel = false;

    // Info stuff
    private UIText _priceText = null;
    private UIText _dustPriceText = null;
    private UIText _chanceText = null;

    static CutJewelUIState()
    {
        _UIItemSlotItemArray ??= typeof(UIItemSlot).GetField("_itemArray", BindingFlags.Instance | BindingFlags.NonPublic);
    }

    public override void Update(GameTime gameTime)
    {
        _storedItem = (_UIItemSlotItemArray.GetValue(_cutSlot) as Item[])[0];
        _hasJewel = _storedItem.ModItem is Jewel;

        string noJewel = "(No jewel selected)";
        float price = _hasJewel ? JewelCutCoinPrice(JewelItem.info) : 0;
        _priceText.SetText(price == 0 ? noJewel : GetPriceText((int)price));

        float dustPrice = _hasJewel ? JewelCutDustPrice(JewelItem.info) : 0;
        _dustPriceText.SetText(price == 0 ? noJewel : dustPrice.ToString() + $"[i:{ModContent.ItemType<SparklyDust>()}]");

        float chance = _hasJewel ? JewelCutChance(JewelItem.info) : -1;
        _chanceText.SetText(chance == -1 ? noJewel : (chance * 100).ToString("#0.##") + "% success chance");
    }

    private static int JewelCutCoinPrice(JewelInfo info) => Item.buyPrice(0, 1) * ((int)info.tier + info.successfulCuts + 1);
    private static int JewelCutDustPrice(JewelInfo info) => ((int)info.tier + 1) * (info.successfulCuts + 1);
    private static float JewelCutChance(JewelInfo info) => 1f - (info.successfulCuts * 0.05f);

    public override void OnInitialize()
    {
        SetDialoguePanel();
        SetCutPanel();
        SetPriceInfoPanel();
    }

    private void SetPriceInfoPanel()
    {
        UIPanel infoPanel = new() // Main back panel
        {
            Width = StyleDimension.FromPixels(200),
            Height = StyleDimension.FromPixels(CutPanelHeight),
            Left = StyleDimension.FromPixelsAndPercent(-264, 0.5f),
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
            Width = StyleDimension.FromPixels(120),
            Height = StyleDimension.FromPixels(CutPanelHeight),
            Top = StyleDimension.FromPercent(0.25f),
            HAlign = 0.5f,
        };
        Append(panel);

        Item air = new();
        air.TurnToAir();
        _cutSlot = new(new Item[] { air }, 0, ItemSlot.Context.ChestItem)
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
        panel.Append(button);

        UIText cutJewelText = new("Cut")
        {
            Top = StyleDimension.FromPixels(-18)
        };
        button.Append(cutJewelText);
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

        info.TryAddCut(JewelCutChance(info));
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
