namespace PeculiarJewelry.Content.JewelryMechanic.Desecration;

public class NPCBehaviourBoostGlobal : GlobalNPC
{
    public override bool InstancePerEntity => true;

    public float extraAISpeed = 0f;

    private float _extraAITimer = 0;
    private bool _boosting = false;

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
        var oldPosition = npc.position;

        while (_extraAITimer >= 1f)
        {
            _boosting = true;
            npc.AI();
            _boosting = false;
            _extraAITimer--;
        }

        npc.position = oldPosition;
    }
}
