using System;

namespace PeculiarJewelry.Content.JewelryMechanic.Desecration.Definitions;

internal class SpawnRateDesecration : DesecrationModifier
{
    public override float StrengthCap => 3f;
    public override float Profanity => 2f;

    public override void ResetEffects(NPC npc) => SpawnNPC.stack = strength;

    private class SpawnNPC : GlobalNPC
    {
        public static float stack = 0;

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (stack == 0)
                return;

            spawnRate = (int)(spawnRate / MathF.Pow(stack, 2));
            maxSpawns = (int)(maxSpawns * (1 + (stack * 0.8f)));
            stack = 0;
        }
    }
}
