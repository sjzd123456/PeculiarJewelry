using System;
using System.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace PeculiarJewelry.Content.JewelryMechanic.UI;

internal class ItemSlotUI : UIItemSlot
{
    private static readonly FieldInfo _UIItemSlotItemArray = null;

    public Item Item => ItemSlots[0];

    private Item[] ItemSlots => (Item[])_UIItemSlotItemArray.GetValue(this);
    private int _context;
    private Func<Item, ItemSlotUI, bool> _handleItem;

    static ItemSlotUI()
    {
        _UIItemSlotItemArray ??= typeof(UIItemSlot).GetField("_itemArray", BindingFlags.Instance | BindingFlags.NonPublic);
    }

    public ItemSlotUI(Item[] itemArray, int itemIndex, int itemSlotContext, Func<Item, ItemSlotUI, bool> handleItemFunc) : base(itemArray, itemIndex, itemSlotContext)
    {
        _context = itemSlotContext;
        _handleItem = handleItemFunc;
    }

    private void HandleItemSlotLogic()
    {
        if (IsMouseHovering)
        {
            Main.LocalPlayer.mouseInterface = true;
            Item inv = ItemSlots[0];

            if (_handleItem.Invoke(Main.LocalPlayer.HeldItem, this))
            {
                ItemSlot.OverrideHover(ref inv, _context);
                ItemSlot.LeftClick(ref inv, _context);
                ItemSlot.RightClick(ref inv, _context);
                ItemSlot.MouseHover(ref inv, _context);
                ItemSlots[0] = inv;
            }
        }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
        HandleItemSlotLogic();
        Item inv = Item;
        Vector2 position = GetDimensions().Center() + new Vector2(52f, 52f) * -0.5f * Main.inventoryScale;
        ItemSlot.Draw(spriteBatch, ref inv, _context, position);
    }
}
