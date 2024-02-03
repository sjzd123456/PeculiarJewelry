using PeculiarJewelry.Content.JewelryMechanic.Desecration;
using System;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace PeculiarJewelry;

public class PeculiarJewelry : Mod
{
    public static JewelryStatConfig StatConfig => ModContent.GetInstance<JewelryStatConfig>();
    
    public static bool ShiftDown
    {
        get
        {
            // Shamelessly taken from https://github.com/SamsonAllen13/ClickerClass/blob/master/Items/ClickerItemCore.cs#L266
            var keys = PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus[TriggerNames.SmartSelect];
            string key = keys.Count == 0 ? null : keys[0];
            return key == null || PlayerInput.Triggers.Current.SmartSelect;
        }
    }

    public override object Call(params object[] args)
    {
        if (args[0] is not string method)
            throw new ArgumentException("Invalid call method!");

        method = method.ToLower();

        if (method == "addrelic")
            RelicTile.Add((int)args[1]);

        return true;
    }
}