namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;

internal enum TriggerContext : byte
{
    None = 0,
    OnTakeDamage,
    OnHeal,
    OnUseMana,
    OnRunOutOfMana,
    OnJump,
    OnHitEnemy,
    OnLand,
    WhenBelowHalfHealth,
    WhenAboveHalfHealth,
    WhenFullHealth,
    WhenBelowHalfMana,
    WhenAboveHalfMana,
    WhenFullMana,
    WhenHaveDebuff,
    WhenOver10Buffs,
    WhenPotionSick,
    WhenNoBuffs,
    WhenIdle,
    WhenNotHitFor15Seconds,
    WhenHitWithinPast5Seconds,
    Max,
}
