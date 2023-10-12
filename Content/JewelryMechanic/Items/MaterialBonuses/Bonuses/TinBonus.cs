namespace PeculiarJewelry.Content.JewelryMechanic.Items.MaterialBonuses.Bonuses;

internal class TinMaterialBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Tin";

    public override float EffectBonus(Player player)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 1)
            return 1.15f;
        return 1f;
    }

    public override void StaticBonus(Player player)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 5)
        {
            player.GetModPlayer<TinMaterialPlayer>().state = TinMaterialPlayer.State.Set5;
            return;
        }
        else if (count >= 3)
            player.GetModPlayer<TinMaterialPlayer>().state = TinMaterialPlayer.State.Set3;
    }

    class TinMaterialPlayer : ModPlayer
    {
        public enum State
        {
            None,
            Set3,
            Set5
        }

        public State state;

        private int _lastHealthHit = 0;

        public override void ResetEffects() => state = State.None;

        public override void PostUpdateEquips()
        {
            if (state == State.Set5)
                Player.GetDamage(DamageClass.Generic) += Player.lifeRegen / 2;
        }

        public override void UpdateLifeRegen()
        {
            if ((state == State.Set3 || state == State.Set5) && Player.statLife < _lastHealthHit && Player.lifeRegen > 0)
                Player.lifeRegen *= 3;
        }

        public override void OnHurt(Player.HurtInfo info) => _lastHealthHit = Player.statLife;
    }
}
