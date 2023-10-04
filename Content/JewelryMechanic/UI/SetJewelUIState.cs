using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using Terraria.UI.Chat;

namespace PeculiarJewelry.Content.JewelryMechanic.UI;

internal class SetJewelUIState : UIState, IClosableUIState
{
    private BasicJewelry Jewelry => _jewelrySlot.Item.ModItem as BasicJewelry;
    private bool HasJewelry => _jewelrySlot.HasItem;

    DynamicSpriteFont Font => FontAssets.MouseText.Value;

    ItemSlotUI _jewelrySlot = null;
    ItemSlotUI[] _jewelSlots = null;
    ItemSlotUI[] _supportSlots = null;
    Item[] _displayJewelItems = null;
    bool[] _displayJewel = null;

    public override void OnInitialize()
    {
        SetDialoguePanel();
        SetSetPanel();
        SetCurrentStatPanel();
        SetFutureStatPanel();
    }

    private void SetFutureStatPanel()
    {
        UIPanel panel = new()
        {
            Width = StyleDimension.FromPixels(300),
            Height = StyleDimension.FromPixels(280),
            HAlign = 0.5f,
            Left = StyleDimension.FromPixels(296),
            Top = StyleDimension.FromPixels(60)
        };
        Append(panel);

        UIText stats = new("No jewelry inserted.")
        {
            IsWrapped = true,
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill,
        };
        stats.OnUpdate += UpdateFutureStats;
        panel.Append(stats);
    }

    private void UpdateFutureStats(UIElement affectedElement)
    {
        UIText self = affectedElement as UIText;
        self.SetText(GetStats(true));

        UIPanel parent = self.Parent as UIPanel;
        var wrapped = Font.CreateWrappedText(self.Text, parent.GetInnerDimensions().Width);
        var size = ChatManager.GetStringSize(Font, wrapped, Vector2.One);
        size = !self.IsWrapped ? new Vector2(size.X, 16f) : new Vector2(size.X, size.Y + self.WrappedTextBottomPadding);
        parent.Height = StyleDimension.FromPixels(size.Y);
        parent.Recalculate();
    }

    private void SetCurrentStatPanel()
    {
        UIPanel panel = new()
        {
            Width = StyleDimension.FromPixels(300),
            Height = StyleDimension.FromPixels(280),
            HAlign = 0.5f,
            Left = StyleDimension.FromPixels(-296),
            Top = StyleDimension.FromPixels(60)
        };
        Append(panel);

        UIText stats = new("No jewelry inserted.")
        {
            IsWrapped = true,
            Width = StyleDimension.Fill,
            Height = StyleDimension.Fill,
        };
        stats.OnUpdate += UpdateCurrentStats;
        panel.Append(stats);
    }

    private void UpdateCurrentStats(UIElement affectedElement)
    {
        UIText self = affectedElement as UIText;
        self.SetText(GetStats(false));

        UIPanel parent = self.Parent as UIPanel;
        var wrapped = Font.CreateWrappedText(self.Text, parent.GetInnerDimensions().Width);
        var size = ChatManager.GetStringSize(Font, wrapped, Vector2.One);
        size = !self.IsWrapped ? new Vector2(size.X, 16f) : new Vector2(size.X, size.Y + self.WrappedTextBottomPadding);
        parent.Height = StyleDimension.FromPixels(size.Y);
        parent.Recalculate();
    }

    private string GetStats(bool isFuture)
    {
        if (!HasJewelry)
            return "No jewelry inserted.";

        if (!Jewelry.Info.Any())
            return "Jewelry has no jewels!";

        string allStats = isFuture ? "[c/00FF00:Will contain:]\n" : "[c/00FF00:Contains:]\n";

        if (isFuture && Jewelry.Info.Count >= Jewelry.Info.Capacity)
            return "[c/FF8888:Jewelry cannot]\naccept more jewels!";

        if (PeculiarJewelry.ShiftDown)
        {
            foreach (var info in Jewelry.Info)
            {
                allStats += info.Name + "\n";

                if (info is MajorJewelInfo major)
                    allStats += major.TriggerTooltip() + "\n";

                allStats += info.Major.GetDescription() + "\n";

                foreach (var item in info.SubStatTooltips())
                    allStats += item + "\n";

                if (Jewelry.Info.IndexOf(info) < Jewelry.Info.Count - 1)
                    allStats += "- - -\n";
            }
        }
        else
        {
            List<TooltipLine> lines = new();
            BasicJewelry.SummaryJewelryTooltips(lines, Jewelry);

            foreach (var item in lines)
                allStats += item.Text + "\n";
        }

        return allStats;
    }

    private void SetSetPanel()
    {
        UIPanel panel = new() // Main back panel
        {
            Width = StyleDimension.FromPixels(280),
            Height = StyleDimension.FromPixels(280),
            Top = StyleDimension.FromPixels(60),
            HAlign = 0.5f,
        };
        Append(panel);

        UIText cutText = new("Jewel Setting")
        {
            HAlign = 0.5f,
            TextColor = Color.Aquamarine,
        };
        panel.Append(cutText);

        _displayJewel = new bool[5] { false, false, false, false, false };
        _displayJewelItems = new Item[5] { null, null, null, null, null };
        Item air = new();
        air.TurnToAir();
        _jewelrySlot = new(new Item[] { air }, 0, ItemSlot.Context.ChestItem, CanJewelrySlotAcceptItem)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(110)
        };
        _jewelrySlot.OnUpdate += UpdateJewelSlots;
        panel.Append(_jewelrySlot);

        UIText jewelryText = new("Jewelry", 0.8f)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(-14)
        };
        _jewelrySlot.Append(jewelryText);

        _jewelSlots = new ItemSlotUI[5];
        for (int i = 0; i < 5; ++i)
        {
            int slot = i;
            _jewelSlots[i] = new(new Item[] { air }, 0, ItemSlot.Context.ChestItem, (Item item, ItemSlotUI _) => CanJewelSlotAcceptItem(item, slot))
            {
                HAlign = 0.5f,
                Left = StyleDimension.FromPixels((i - 2) * 52),
                Top = StyleDimension.FromPixels(42 + (Math.Abs(i - 2) * 20))
            };
            panel.Append(_jewelSlots[i]);
        }

        UIText jewelText = new("Jewels", 0.8f)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(-14)
        };
        _jewelSlots[2].Append(jewelText);

        _supportSlots = new ItemSlotUI[3];
        for (int i = 0; i < 3; ++i)
        {
            _supportSlots[i] = new(new Item[] { air }, 0, ItemSlot.Context.ChestItem, CutJewelUIState.CanJewelSlotAcceptItem)
            {
                HAlign = 0.5f,
                Left = StyleDimension.FromPixels((i - 1) * 52),
                Top = StyleDimension.FromPixels(210)
            };
            panel.Append(_supportSlots[i]);
        }

        UIImageButton button = new(TextureAssets.Item[ItemID.IronAnvil])
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(160),
            Width = StyleDimension.FromPixels(30),
            Height = StyleDimension.FromPixels(30),
        };
        button.OnLeftClick += SetJewel;
        panel.Append(button);

        UIText supportText = new("Support Items", 0.8f)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(-14)
        };
        _supportSlots[1].Append(supportText);
    }

    private bool CanJewelSlotAcceptItem(Item item, int i)
    {
        if (_displayJewel[i])
            return false;

        bool isMouseItem = Main.LocalPlayer.selectedItem == 58 && Main.LocalPlayer.HeldItem == item;
        return item.ModItem is Jewel || item.IsAir || !isMouseItem;
    }

    private void UpdateJewelSlots(UIElement affectedElement)
    {
        if (!HasJewelry)
        {
            for (int i = 0; i < _jewelSlots.Length; i++)
            {
                var self = _jewelSlots[i];

                if (!self.Item.IsAir)
                    self.Item.TurnToAir();
            }

            _displayJewelItems = new Item[5] { null, null, null, null, null };
        }
        else
        {
            _displayJewel = new bool[5] { false, false, false, false, false };

            for (int i = 0; i < _jewelSlots.Length; i++)
            {
                if (Jewelry.Info.Count <= i)
                    break;
                
                var self = _jewelSlots[i];
                var info = Jewelry.Info[i];

                if (_displayJewelItems[i] is null)
                {
                    Item item = new(info is MajorJewelInfo ? ModContent.ItemType<MajorJewel>() : ModContent.ItemType<MinorJewel>());
                    (item.ModItem as Jewel).info = info;
                    _displayJewelItems[i] = item;
                }

                _displayJewel[i] = true;
                self.ForceItem(_displayJewelItems[i].Clone());
            }
        }
    }

    private void SetDialoguePanel()
    {
        Append(new UINPCDialoguePanel()
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(174),
            Width = StyleDimension.FromPixels(280),
            Height = StyleDimension.FromPixels(600)
        });
    }

    private void SetJewel(UIMouseEvent evt, UIElement listeningElement)
    {
        if (_jewelrySlot.Item.IsAir)
            return;

        if (!_jewelSlots.Any(x => !x.Item.IsAir))
            return;

        if (Jewelry.Info.Count >= Jewelry.Info.Capacity || Jewelry.Info.Count >= 5)
            return;

        for (int i = 0; i < Jewelry.Info.Capacity; ++i) 
        {
            if (!_jewelSlots[i].HasItem || _displayJewel[i])
                continue;

            var jewel = _jewelSlots[i].Item.ModItem as Jewel;
            Jewelry.Info.Add(jewel.info);
            _jewelSlots[i].Item.TurnToAir();

            if (Jewelry.Info.Count >= Jewelry.Info.Capacity || Jewelry.Info.Count >= 5)
                break;
        }

        Main.npcChatText = "There you are! Proud of this one.";
    }

    private static bool CanJewelrySlotAcceptItem(Item item, ItemSlotUI _)
    {
        bool isMouseItem = Main.LocalPlayer.selectedItem == 58 && Main.LocalPlayer.HeldItem == item;
        return item.ModItem is BasicJewelry || item.IsAir || !isMouseItem;
    }

    public void Close()
    {
        if (_jewelrySlot.HasItem)
            Main.LocalPlayer.QuickSpawnItem(new EntitySource_OverfullInventory(Main.LocalPlayer), _jewelrySlot.Item);

        foreach (var slot in _jewelSlots)
        {
            if (slot.HasItem)
                Main.LocalPlayer.QuickSpawnItem(new EntitySource_OverfullInventory(Main.LocalPlayer), slot.Item);
        }

        foreach (var slot in _supportSlots)
        {
            if (slot.HasItem)
                Main.LocalPlayer.QuickSpawnItem(new EntitySource_OverfullInventory(Main.LocalPlayer), slot.Item);
        }
    }
}