using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public static void SwitchUI(UIState state)
    {
        if (Instance.JewelInterface.CurrentState is IClosableUIState close)
            close.Close();

        Instance.JewelInterface.SetState(state);
    }

    public override void UpdateUI(GameTime gameTime) => JewelInterface.Update(gameTime);

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
