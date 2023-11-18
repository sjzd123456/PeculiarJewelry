using PeculiarJewelry.Content.JewelryMechanic.Items.Jewels;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace PeculiarJewelry.Content.JewelryMechanic.UI.Superimposition;

internal class SuperimpositionUIState : UIState, IClosableUIState
{
    private readonly List<JewelStat> _storedStats = new();
    private readonly Dictionary<JewelStat, bool> _storedStatsSide = new();

    private ItemSlotUI _leftJewel;
    private ItemSlotUI _rightJewel;
    private ItemSlotUI _resultJewel;
    private JewelSubstatUI _leftStats;
    private JewelSubstatUI _rightStats;
    private bool _minor = false;

    internal static string Localize(string postfix) => Language.GetTextValue("Mods.PeculiarJewelry.UI.SuperimpositionMenu." + postfix);

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (_leftJewel.Item.ModItem is Jewel jewel && !_leftStats.Showing)
            _leftStats.RebuildStats(jewel);

        if (_leftJewel.Item.ModItem is not Jewel && _leftStats.Showing)
        {
            _leftStats.Hide(true);
            ClearUnusedStats(true);
        }

        if (_rightJewel.Item.ModItem is Jewel rightJewel && !_rightStats.Showing)
            _rightStats.RebuildStats(rightJewel);

        if (_rightJewel.Item.ModItem is not Jewel && _rightStats.Showing)
        {
            _rightStats.Hide(true);
            ClearUnusedStats(false);
        }

        _minor = _leftJewel.Item.ModItem is MinorJewel || _rightJewel.Item.ModItem is MinorJewel;
    }

    private void ClearUnusedStats(bool left)
    {
        List<JewelStat> stats = new();
        foreach (var pair in _storedStatsSide)
            if (pair.Value == left)
                stats.Add(pair.Key);

        foreach (var item in stats)
        {
            _storedStatsSide.Remove(item);
            _storedStats.Remove(item);
        }
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
            Height = StyleDimension.FromPixels(430),
            Top = StyleDimension.FromPixels(30),
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

        _leftStats = new JewelSubstatUI((stat) => TryAddStat(stat, true))
        {
            Width = StyleDimension.FromPixels(134),
            Height = StyleDimension.FromPercent(0.38f),
            Top = StyleDimension.FromPixels(70)
        };
        panel.Append(_leftStats);

        _rightStats = new JewelSubstatUI((stat) => TryAddStat(stat, false))
        {
            Width = StyleDimension.FromPixels(134),
            Height = StyleDimension.FromPercent(0.38f),
            Top = StyleDimension.FromPixels(70),
            HAlign = 1f,
        };
        panel.Append(_rightStats);

        panel.Append(new UIText("Effects")
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(230)
        });

        panel.Append(new UIText("Result")
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(320)
        });

        _resultJewel = new ItemSlotUI(new Item[] { air }, 0, ItemSlot.Context.ChestItem, (item, ui) => true)
        {
            HAlign = 0.5f,
            Top = StyleDimension.FromPixels(346)
        };
        panel.Append(_resultJewel);
    }

    private void TryAddStat(JewelStat stat, bool left)
    {
        if (_minor)
        {
            if (_storedStats.Any())
                _storedStats.RemoveAt(0);

            _storedStats.Add(stat);
        }
        else
        {
            if (!_storedStats.Contains(stat))
            {
                var side = _storedStats.FirstOrDefault(x =>
                {
                    KeyValuePair<JewelStat, bool>? first = _storedStatsSide.FirstOrDefault(x => x.Value == left);

                    if (first is null)
                        return false;

                    return x == first.Value.Key;
                });

                if (side is not null)
                {
                    _storedStatsSide.Remove(side);
                    _storedStats.Remove(side);
                }

                if (!_storedStatsSide.ContainsValue(left))
                {
                    _storedStats.Add(stat);
                    _storedStatsSide.Add(stat, left);
                }
            }
        }

        Color select = Color.Lerp(new Color(210, 210, 90), UICommon.DefaultUIBlue, 0.6f);
        _leftStats.Highlight(select, UICommon.DefaultUIBlue, _storedStats);
        _rightStats.Highlight(select, UICommon.DefaultUIBlue, _storedStats);
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
            Top = StyleDimension.FromPixels(248),
            Width = StyleDimension.FromPixels(300),
            Height = StyleDimension.FromPixels(600)
        });
    }

    public void Close()
    {
        if (_leftJewel.HasItem)
            Main.LocalPlayer.QuickSpawnItem(new EntitySource_OverfullInventory(Main.LocalPlayer), _leftJewel.Item, _leftJewel.Item.stack);

        if (_rightJewel.HasItem)
            Main.LocalPlayer.QuickSpawnItem(new EntitySource_OverfullInventory(Main.LocalPlayer), _rightJewel.Item, _rightJewel.Item.stack);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        float oldScale = Main.inventoryScale;
        Main.inventoryScale = 0.9f;
        base.Draw(spriteBatch);
        Main.inventoryScale = oldScale;
    }
}
