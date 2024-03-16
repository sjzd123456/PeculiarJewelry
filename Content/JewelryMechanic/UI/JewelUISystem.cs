using System.Collections.Generic;
using Terraria.UI;

namespace PeculiarJewelry.Content.JewelryMechanic.UI;

internal class JewelUISystem : ModSystem
{
    public static JewelUISystem Instance => ModContent.GetInstance<JewelUISystem>();

    internal UserInterface JewelInterface;

    public override void Load()
    {
        if (!Main.dedServ)
        {
            JewelInterface = new UserInterface();
            JewelInterface.SetState(null);
        }
    }

    public override void OnWorldUnload() => SwitchUI(null, false);

    public static void SwitchUI(UIState state, bool closeChat = true)
    {
        if (Instance.JewelInterface is null)
            return;

        if (Instance.JewelInterface.CurrentState is IClosableUIState close)
            close.Close();

        Instance.JewelInterface.SetState(state);

        if (closeChat)
        {
            string old = Main.npcChatText;
            Main.npcChatText = string.Empty;
            Main.CloseNPCChatOrSign(); // npcChatText needs to be nothing in order to close (??)
            Main.npcChatText = old;
        }
    }

    public override void UpdateUI(GameTime gameTime)
    {
        if (Main.LocalPlayer.talkNPC >= 0 && JewelInterface.CurrentState is not null && JewelInterface.CurrentState is not ChooseJewelMechanicUIState)
            SwitchUI(null, false);

        JewelInterface.Update(gameTime);
    }

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        int resourceBarIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Resource Bars"));

        if (resourceBarIndex != -1)
        {
            layers.Insert(resourceBarIndex, new LegacyGameInterfaceLayer(
                "PeculiarJewelry: Lapidarist UI",
                delegate
                {
                    JewelInterface.Draw(Main.spriteBatch, Main.gameTimeCache);
                    return true;
                },
                InterfaceScaleType.UI)
            );
        }
    }
}
