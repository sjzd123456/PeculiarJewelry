using NetEasy;
using PeculiarJewelry.Content.JewelryMechanic.Desecration;
using System;

namespace PeculiarJewelry.Content.JewelryMechanic.Syncing;

[Serializable]
internal class DesecrationModule(string deseKey, float str, int from) : Module
{
    private readonly string key = deseKey;
    private readonly float strength = str;
    private readonly int fromWho = from;

    protected override void Receive()
    {
        ModContent.GetInstance<DesecratedSystem>().SetDesecration(key, strength);

        if (Main.netMode == NetmodeID.Server && fromWho != -1) //Send to other clients
        {
            if (fromWho < 0 || fromWho >= Main.maxPlayers)
                return;

            Send(-1, fromWho, false);
        }
    }
}
