namespace PeculiarJewelry.Content.JewelryMechanic.Desecration.Definitions;

internal class LongerHealCooldownDesecration : DesecrationModifier
{
    public override float StrengthCap => 3f;
    public override float Profanity => 2f;

    public override void Load() => On_Player.AddBuff += IncreaseHealCooldown;

    private void IncreaseHealCooldown(On_Player.orig_AddBuff orig, Player self, int type, int timeToAdd, bool quiet, bool foodHack)
    {
        if (Desecrations["LongerHealCooldownDesecration"].strength > 0)
            timeToAdd = (int)(timeToAdd * (1 + (strength * 0.25f)));

        orig(self, type, timeToAdd, quiet, foodHack);
    }
}
