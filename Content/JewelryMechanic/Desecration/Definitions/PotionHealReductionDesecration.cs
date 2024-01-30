namespace PeculiarJewelry.Content.JewelryMechanic.Desecration.Definitions;

internal class PotionHealReductionDesecration : DesecrationModifier
{
    public override float StrengthCap => 3f;

    private class HealingPlayer : ModPlayer
    {
        public override void GetHealLife(Item item, bool quickHeal, ref int healValue)
        {
            float factor = 1 - (Desecrations["PotionHealReductionDesecration"].strength * 0.25f);
            healValue = (int)(healValue * factor);
        }
    }
}
