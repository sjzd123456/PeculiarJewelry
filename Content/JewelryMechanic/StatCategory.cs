namespace PeculiarJewelry.Content.JewelryMechanic;

internal enum StatCategory
{
    /// <summary>
    /// Max health.
    /// </summary>
    Vigor,

    /// <summary>
    /// Life regeneration.
    /// </summary>
    Renewal,
    
    /// <summary>
    /// Defense.
    /// </summary>
    Permenance,

    /// <summary>
    /// Damage reduction.
    /// </summary>
    Tenaticy,

    /// <summary>
    /// Critical strike chance.
    /// </summary>
    Exactitude,
    
    /// <summary>
    /// Critical strike damage.
    /// </summary>
    Exploitation,

    /// <summary>
    /// Speed, flight time and max jump height, or just velocity.
    /// </summary>
    Celerity,

    /// <summary>
    /// Horizontal and vertical acceleration and turnaround time, or just movement control.
    /// </summary>
    Dexterity,

    /// <summary>
    /// Melee damage.
    /// MELEE CLASS.
    /// </summary>
    Might,

    /// <summary>
    /// Melee weapon size.
    /// MELEE CLASS.
    /// </summary>
    Gigantism,

    /// <summary>
    /// Use time.
    /// MELEE CLASS.
    /// </summary>
    Frenzy,

    /// <summary>
    /// Ranged damage.
    /// RANGED CLASS.
    /// </summary>
    Precision,

    /// <summary>
    /// Ammo consumption chance.
    /// RANGED CLASS.
    /// </summary>
    Preservation,

    /// <summary>
    /// Projectile speed.
    /// RANGED CLASS.
    /// </summary>
    Tension,

    /// <summary>
    /// Magic damage.
    /// MAGIC CLASS.
    /// </summary>
    Willpower,

    /// <summary>
    /// Max mana.
    /// MAGIC CLASS.
    /// </summary>
    Arcane,

    /// <summary>
    /// Mana regeneration.
    /// MAGIC CLASS.
    /// </summary>
    Resurgence,

    /// <summary>
    /// Summoner damage.
    /// SUMMON CLASS.
    /// </summary>
    Order,

    /// <summary>
    /// Max minions.
    /// SUMMON CLASS.
    /// </summary>
    Abundance,

    /// <summary>
    /// Knockback.
    /// SUMMON CLASS.
    /// </summary>
    Turbulance,
    
    /// <summary>
    /// All damage.
    /// GENERIC CLASS.
    /// </summary>
    Potency,

    /// <summary>
    /// Defense piercing.
    /// GENERIC CLASS.
    /// </summary>
    Absolution
}
