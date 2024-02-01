using System.Reflection;

namespace PeculiarJewelry.Content.JewelryMechanic.Desecration;

public class NPCBehaviourBoostGlobal : GlobalNPC
{
    private static MethodInfo CollisionMethod;

    public override bool InstancePerEntity => true;

    public float extraAISpeed = 0f;

    private float _extraAITimer = 0;
    private bool _boosting = false;

    public override void Load() => CollisionMethod = typeof(NPC).GetMethod("UpdateCollision", BindingFlags.Instance | BindingFlags.NonPublic);

    public override void ResetEffects(NPC npc)
    {
        _boosting = false;
        extraAISpeed = 0;
    }

    public override void PostAI(NPC npc)
    {
        if (_boosting)
            return;

        if (extraAISpeed == 0)
        {
            _extraAITimer = 0;
            return;
        }

        _extraAITimer += extraAISpeed;

        while (_extraAITimer >= 1f)
        {
            _boosting = true;

            CollisionMethod.Invoke(npc, null); // Force collision check to try and keep npcs on the ground
            npc.position += npc.velocity;
            NPCLoader.AI(npc);
            _boosting = false;
            _extraAITimer--;
        }
    }
}
