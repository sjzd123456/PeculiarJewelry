using PeculiarJewelry.Content.JewelryMechanic.Stats;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class LuminiteBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Luminite";

    public override int GetMajorJewelCount => 2;

    public override bool AppliesToStat(Player player, StatType type) => true;
    public override float EffectBonus(Player player, StatType statType) => 0.85f;

    public override void StaticBonus(Player player, bool firstSet)
    {
        if (firstSet && CountMaterial(player) >= 3)
            AddCosmicBonus(player);            
    }

    public static void AddCosmicBonus(Player player)
    {
        GetQuadrantBonus(player);
        GetTimeBonus(player);
    }

    private static void GetTimeBonus(Player player)
    {
        double time = Main.time; // Calculates time as ##:## on a 24-hour format

        if (!Main.dayTime)
            time += 54000.0;

        time = time / 86400.0 * 24.0;
        time = time - 7.5 - 12.0;

        if (time < 0.0)
            time += 24.0;

        int hour = (int)time;
        double minute = time - hour;
        minute = (int)(minute * 60.0);

        bool Within(int startHour, int endHour) => (hour == startHour && minute >= 30) || (hour == endHour && minute < 30);

        const float Bonus = 0.2f;

        if (Within(4, 5) || Within(10, 11) || Within(16, 17) || Within(22, 23)) // Actually apply the bonuses
            player.GetDamage(DamageClass.Melee) += Bonus;
        else if (Within(5, 6) || Within(11, 12) || Within(17, 18) || Within(23, 24))
            player.GetDamage(DamageClass.Ranged) += Bonus;
        else if (Within(6, 7) || Within(12, 13) || Within(18, 19) || Within(0, 1))
            player.GetDamage(DamageClass.Magic) += Bonus;
        else if (Within(7, 8) || Within(13, 14) || Within(19, 20) || Within(1, 2))
            player.GetDamage(DamageClass.Summon) += Bonus;
        else if (Within(8, 9) || Within(14, 15) || Within(20, 21) || Within(2, 3))
            player.GetModPlayer<LuminiteBonusPlayer>().critDamageBonus += Bonus;
        else
            player.GetDamage(DamageClass.Generic).Flat += 5;
    }

    private static void GetQuadrantBonus(Player player)
    {
        bool left = player.Center.X < Main.maxTilesX * 8;
        bool top = player.Center.Y < Main.maxTilesY * 8;

        if (!left && !top)
            player.endurance += 0.15f;
        else if (left && top)
            player.lifeRegen += 16;
        else if (!left && top)
            player.statLifeMax2 += 200;
        else
            player.statDefense += 20;
    }

    //Needs 5-Set

    private class LuminiteBonusPlayer : ModPlayer 
    {
        internal float critDamageBonus = 0;

        public override void ResetEffects() => critDamageBonus = 0;

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            if (critDamageBonus > 0)
                modifiers.CritDamage += critDamageBonus;
        }
    }
}
