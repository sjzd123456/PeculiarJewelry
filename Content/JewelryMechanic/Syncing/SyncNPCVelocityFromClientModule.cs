using NetEasy;
using System;

namespace PeculiarJewelry.Content.JewelryMechanic.Syncing;

[Serializable]
public class SyncNPCVelocityFromClientModule(int npcWho, Vector2 velocity) : Module
{
    private readonly int _npcWho = npcWho;
    private readonly float x = velocity.X;
    private readonly float y = velocity.Y;

    protected override void Receive()
    {
        Main.npc[_npcWho].velocity = new(x, y);
    }
}
