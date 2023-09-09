using System.Collections.Generic;

namespace PeculiarJewelry.Content.JewelryMechanic.Stats.Triggers;

internal abstract class TriggerEffect : ModType
{
    private static readonly Dictionary<TriggerContext, float> ConditionCoefficients = new()
    {
        { TriggerContext.OnTakeDamage, 0.5f },
        { TriggerContext.OnHeal, 5f },
        { TriggerContext.OnUseMana, 0.2f },
        { TriggerContext.OnRunOutOfMana, 1 },
        { TriggerContext.OnJump, 0.1f },
        { TriggerContext.OnHitEnemy, 0.1f },
        { TriggerContext.OnLand, 0.2f },
        { TriggerContext.WhenBelowHalfHealth, 0.8f },
        { TriggerContext.WhenAboveHalfHealth, 0.5f },
        { TriggerContext.WhenFullHealth, 1 },
        { TriggerContext.WhenBelowHalfMana, 0.4f },
        { TriggerContext.WhenAboveHalfMana, 0.25f },
        { TriggerContext.WhenFullMana, 0.5f },
        { TriggerContext.WhenHaveDebuff, 0.5f },
        { TriggerContext.WhenOver10Buffs, 0.5f },
        { TriggerContext.WhenPotionSick, 0.2f },
        { TriggerContext.WhenNoBuffs, 1 },
        { TriggerContext.WhenIdle, 1 },
        { TriggerContext.WhenNotHitFor15Seconds, 0.5f },
        { TriggerContext.WhenHitWithinPast5Seconds, 1 }
    };

    public abstract TriggerType Type { get; }

    public TriggerContext Context { get; protected set; }

    public TriggerEffect()
    {
        Context = (TriggerContext)Main.rand.Next((int)TriggerContext.Max);
    }

    protected override void Register() => ModTypeLookup<TriggerEffect>.Register(this);

    public void InstantTrigger(TriggerContext context, Player player, JewelInfo.JewelTier tier)
    {
        int tierInt = (int)tier;

        if (Type == TriggerType.InstantOther)
        {
            if ((tierInt + 1) / (float)(tierInt + 3) > Main.rand.NextFloat())
            {
                float coefficient = ConditionCoefficients[context];
                InternalInstantOtherEffect(context, player, coefficient);
            }
        }
        else
        {
            if (tierInt / (float)(tierInt + 1) > Main.rand.NextFloat())
            {
                float coefficient = ConditionCoefficients[context];
                InternalInstantOtherEffect(context, player, coefficient);
            }
        }
    }

    protected virtual void InternalInstantOtherEffect(TriggerContext context, Player player, float coefficient) { }

    public void ConstantTrigger(Player player, JewelInfo.JewelTier tier)
    {
        if (ConstantConditionMet(Context, player, tier))
        {
            float coefficient = ConditionCoefficients[Context];
            InternalConditionalEffect(Context, player, coefficient);
        }
    }

    protected virtual bool ConstantConditionMet(TriggerContext context, Player player, JewelInfo.JewelTier tier) => false;
    protected virtual void InternalConditionalEffect(TriggerContext context, Player player, float coefficient) { }

    public string Tooltip(TriggerContext context, JewelInfo.JewelTier tier)
    {
        float coefficient = ConditionCoefficients[context];
        string condition = Language.GetText("Mods.PeculiarJewelry.Jewelry.TriggerContexts." + context).Value;
        string effect = Language.GetText("Mods.PeculiarJewelry.Jewelry.TriggerEffects." + GetType().Name).WithFormatArgs(TooltipArgument(coefficient, tier)).Value;
        return condition + " " + effect;
    }

    public abstract string TooltipArgument(float coefficient, JewelInfo.JewelTier tier);
}
