using NetEasy;
using PeculiarJewelry.Content.JewelryMechanic.Desecration;
using System;

namespace PeculiarJewelry.Content.JewelryMechanic.Syncing;

[Serializable]
public class SyncDesecrationModule(int from) : Module
{
    private readonly int fromWho = from;

    protected override void Receive()
    {
        if (Main.netMode == NetmodeID.Server) //Send to other clients
        {
            if (fromWho < 0 || fromWho >= Main.maxPlayers)
                return;

            foreach (var item in ModContent.GetInstance<DesecratedSystem>().Desecrations)
                new DesecrationModule(item.Key, item.Value.strength, -1).Send(fromWho, -1, false);
        }
    }
}

internal class SyncPlayer : ModPlayer
{
    public override void OnEnterWorld()
    {
        if (Main.netMode != NetmodeID.SinglePlayer)
            new SyncDesecrationModule((byte)Main.myPlayer).Send();
    }
}
