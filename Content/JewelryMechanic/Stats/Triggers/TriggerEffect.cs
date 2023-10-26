﻿using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
    public virtual bool NeedsCooldown => false;

    public int CooldownBuffType => !NeedsCooldown ? throw new FieldAccessException($"{GetType().Name} has no buff!")
        : ModLoader.GetMod("PeculiarJewelry").Find<ModBuff>(GetType().Name + "Buff").Type;

    public TriggerContext Context { get; protected set; }

    public TriggerEffect()
    {
        Context = TriggerContext.OnHitEnemy;// (TriggerContext)Main.rand.Next((int)TriggerContext.Max);
    }

    protected sealed override void Register()
    {
        ModTypeLookup<TriggerEffect>.Register(this);

        if (NeedsCooldown)
        {
            string key = "Mods.PeculiarJewelry.Jewelry.TriggerEffects." + GetType().Name + "Buff.";
            Mod.AddContent(new TriggerCooldownBuff(GetType().Name + "Buff", Language.GetText(key + "BuffName"), Language.GetText(key + "BuffDescription")));
        }
    }

    internal void ForceSetContext(TriggerContext context) => Context = context;

    public static int CooldownTime(JewelTier tier) => (int)Math.Pow(2, 1 - ((float)tier / 10));

    public void InstantTrigger(TriggerContext context, Player player, JewelTier tier)
    {
        if (NeedsCooldown && player.HasBuff(CooldownBuffType))
            return;

        if (Type == TriggerType.InstantOther)
        {
            if (ReportInstantChance(tier, player) > Main.rand.NextFloat())
            {
                float coefficient = ConditionCoefficients[context];
                float bonus = player.GetModPlayer<MaterialPlayer>().CompoundCoefficientTriggerBonuses();

                InternalInstantOtherEffect(context, player, coefficient * bonus, tier);
            }
        }
        //else
        //{
        //    if (tierInt / (float)(tierInt + 1) > Main.rand.NextFloat())
        //    {
        //        float coefficient = ConditionCoefficients[context];
        //        InternalInstantOtherEffect(context, player, coefficient);
        //    }
        //}
    }

    protected virtual void InternalInstantOtherEffect(TriggerContext context, Player player, float coefficient, JewelTier tier) { }

    public void ConstantTrigger(Player player, JewelTier tier)
    {
        if (NeedsCooldown && player.HasBuff(CooldownBuffType))
            return;

        if (ConstantConditionMet(Context, player, tier))
        {
            float coefficient = ConditionCoefficients[Context];
            InternalConditionalEffect(Context, player, coefficient);
        }
    }

    protected virtual bool ConstantConditionMet(TriggerContext context, Player player, JewelTier tier) => false;
    protected virtual void InternalConditionalEffect(TriggerContext context, Player player, float coefficient) { }

    public virtual string Tooltip(JewelTier tier, Player player)
    {
        string condition = Language.GetText("Mods.PeculiarJewelry.Jewelry.TriggerContexts." + Context).Value;
        string chance = Language.GetText("Mods.PeculiarJewelry.Jewelry.ChanceTo").WithFormatArgs((ReportInstantChance(tier, player) * 100).ToString("#0.##")).Value;
        string effect = Language.GetText("Mods.PeculiarJewelry.Jewelry.TriggerEffects." + GetType().Name).WithFormatArgs(TriggerPower(tier)).Value;
        return condition + " " + chance + effect;
    }

    private static float ReportInstantChance(JewelTier jewelTier, Player player)
    {
        int tier = (int)jewelTier;
        float chance = (tier + 1f) / (tier + 3f);
        int meteoriteCount = player.GetModPlayer<MaterialPlayer>().MaterialCount("Meteorite");
        chance += (100 - (chance * 100)) / 100 * (meteoriteCount / (meteoriteCount + 3f));

        return chance;
    }

    public virtual string TooltipArgumentFormat(float coefficient, JewelTier tier) => (TriggerPower(tier) * coefficient).ToString("#0.##");
    public abstract float TriggerPower(JewelTier tier);

    public float TotalPower(Player player, float coefficient, JewelTier tier) 
    {
        float hellstoneMultiplier = player.GetModPlayer<MaterialPlayer>().MaterialCount("Hellstone") * 0.5f;
        return coefficient * TriggerPower(tier) * (hellstoneMultiplier + 1);
    } 
}
