using PeculiarJewelry.Content.JewelryMechanic.Items.JewelryItems;
using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using System;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace PeculiarJewelry.Content.JewelryMechanic.UI;

internal class SetJewelUIState : UIState, IClosableUIState
{
    ItemSlotUI _jewelrySlot = null;
    ItemSlotUI[] _jewelSlots = null;
    ItemSlotUI[] _supportSlots = null;

    public override void Update(GameTime gameTime)
    {
    }

    public override void OnInitialize()
    {
        UIPanel panel = new() // Main back panel
        {
            Width = StyleDimension.FromPixels(280),
            Height = StyleDimension.FromPixels(220),
            Top = StyleDimension.FromPercent(0.25f),
            HAlign = 0.5f,
        };
        Append(panel);

        Item air = new();
        air.TurnToAir();
        _jewelrySlot = new(new Item[] { air }, 0, ItemSlot.Context.ChestItem, CanJewelSlotAcceptItem)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(70)
        };
        panel.Append(_jewelrySlot);

        UIText jewelryText = new("Jewelry", 0.8f)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(-8)
        };
        _jewelrySlot.Append(jewelryText);

        _jewelSlots = new ItemSlotUI[5];
        for (int i = 0; i < 5; ++i)
        {
            _jewelSlots[i] = new(new Item[] { air }, 0, ItemSlot.Context.ChestItem, CutJewelUIState.CanJewelSlotAcceptItem)
            {
                HAlign = 0.5f,
                Left = StyleDimension.FromPixels((i - 2) * 52),
                Top = StyleDimension.FromPixels(12 + (Math.Abs(i - 2) * 20))
            };
            panel.Append(_jewelSlots[i]);
        }

        UIText jewelText = new("Jewels", 0.8f)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(-8)
        };
        _jewelSlots[2].Append(jewelText);

        _supportSlots = new ItemSlotUI[3];
        for (int i = 0; i < 3; ++i)
        {
            _supportSlots[i] = new(new Item[] { air }, 0, ItemSlot.Context.ChestItem, CutJewelUIState.CanJewelSlotAcceptItem)
            {
                HAlign = 0.5f,
                Left = StyleDimension.FromPixels((i - 1) * 52),
                Top = StyleDimension.FromPixels(150)
            };
            panel.Append(_supportSlots[i]);
        }

        UIImageButton button = new(TextureAssets.Item[ItemID.IronAnvil])
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(120),
            Width = StyleDimension.FromPixels(30),
            Height = StyleDimension.FromPixels(30),
        };
        button.OnLeftClick += SetJewel;
        panel.Append(button);

        UIText supportText = new("Support Items", 0.8f)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(-8)
        };
        _supportSlots[1].Append(supportText);
    }

    private void SetJewel(UIMouseEvent evt, UIElement listeningElement)
    {
        if (_jewelrySlot.Item.IsAir)
            return;

        if (!_jewelSlots.Any(x => !x.Item.IsAir))
            return;

        var jewelry = _jewelrySlot.Item.ModItem as BasicJewelry;

        if (jewelry.Info.Count >= jewelry.Info.Capacity)
            return;

        for (int i = 0; i < jewelry.Info.Capacity; ++i) 
        {
            if (!_jewelSlots[i].HasItem)
                continue;

            var jewel = _jewelSlots[i].Item.ModItem as Jewel;
            jewelry.Info.Add(jewel.info);
            _jewelSlots[i].Item.TurnToAir();
        }
    }

    private static bool CanJewelSlotAcceptItem(Item item, ItemSlotUI _)
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