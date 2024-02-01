using System;
using System.Collections.Generic;

namespace PeculiarJewelry.Content.JewelryMechanic.Desecration;

public class DesecratedSystem : ModSystem
{
    private readonly Dictionary<string, DesecrationModifier> Desecrations = [];

    public static float TotalProfanity => ModContent.GetInstance<DesecratedSystem>()._totalProfanity;
    public static float LootScaleFactor => ModContent.GetInstance<DesecratedSystem>()._totalProfanity * 20 / 100f;
    public static int AdditionalJewelTier => (int)(ModContent.GetInstance<DesecratedSystem>()._totalProfanity / 2f);

    private float _totalProfanity = 0;

    /// <summary>
    /// Adds strength to a desecration. Returns false if the cap is reached.
    /// </summary>
    /// <param name="key">Desecration to add to.</param>
    /// <returns>False if the cap is reached.</returns>
    /// <exception cref="ArgumentException"/>
    public bool AddDesecration(string key)
    {
        if (!DesecrationModifier.Desecrations.ContainsKey(key))
            throw new ArgumentException($"No desecration by the name of {key} exists.");

        if (Desecrations.ContainsKey(key))
            Desecrations[key].strength++;
        else
        {
            DesecrationModifier.Desecrations[key].strength = 1;
            Desecrations.Add(key, DesecrationModifier.Desecrations[key]);
        }

        if (Desecrations[key].StrengthCap != -1 && Desecrations[key].strength >= Desecrations[key].StrengthCap)
        {
            Desecrations[key].strength = Desecrations[key].StrengthCap;
            return false;
        }

        _totalProfanity++;
        return true;
    }

    public void SetDesecration(string key, float strength)
    {
        DesecrationModifier.Desecrations[key].strength = strength;

        if (strength <= 0)
            Desecrations.Remove(key);
        else
        {
            if (Desecrations.ContainsKey(key))
                Desecrations[key].strength = strength;
            else
                Desecrations.Add(key, DesecrationModifier.Desecrations[key]);
        }

        GetTotalProfanity();
    }

    private void GetTotalProfanity()
    {
        _totalProfanity = 0;

        foreach (var item in Desecrations.Values)
            _totalProfanity += item.strength * item.Profanity;
    }

    public void ClearDesecrations()
    {
        foreach (var item in Desecrations.Values)
            item.strength = 0;

        Desecrations.Clear();
    }

    internal class DesecrationNPC : GlobalNPC
    {
        private static DesecratedSystem System => ModContent.GetInstance<DesecratedSystem>();

        public override bool AppliesToEntity(NPC npc, bool lateInstantiation) => !npc.friendly && !npc.townNPC && npc.lifeMax > 5;

        public override void SetDefaults(NPC npc)
        {
            if (!AppliesToEntity(npc, false))
                return;

            foreach (var item in System.Desecrations.Values)
                item.PostSetDefaults(npc);
        }

        public override bool PreAI(NPC npc)
        {
            if (!AppliesToEntity(npc, false))
                return true;

            foreach (var item in System.Desecrations.Values)
                item.PreAI(npc);

            return true;
        }

        public override void ResetEffects(NPC npc)
        {
            if (!AppliesToEntity(npc, false))
                return;

            foreach (var item in System.Desecrations.Values)
                item.ResetEffects(npc);
        }
    }
}