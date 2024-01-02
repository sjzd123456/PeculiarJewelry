using System.Collections.Generic;
using System.Linq;
using Terraria.GameContent;
using Terraria.GameContent.Personalities;
using Terraria.GameContent.Bestiary;
using PeculiarJewelry.Content.JewelryMechanic.UI;
using PeculiarJewelry.Content.Items.Jewels;
using System;
using PeculiarJewelry.Content.Items;
using PeculiarJewelry.Content.Items.Pliers;
using Terraria;
using System.Collections.ObjectModel;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using PeculiarJewelry.Content.JewelryMechanic;
using Terraria.Utilities;
using PeculiarJewelry.Content.Items.JewelSupport;

namespace PeculiarJewelry.Content.NPCs;

[AutoloadHead]
public class Lapidarist : ModNPC
{
    //private WeightedRandom<(int type, int stack, int price)> MiscPool 
    //{
    //    get
    //    {
    //        var pool = new WeightedRandom<(int type, int stack, int price)>();
    //        pool.Add((ModContent.ItemType<CursedDollar>(), 10, Item.buyPrice(gold: 25)), 1);
    //        pool.Add((ModContent.ItemType<IrradiatedPearl>(), 5, Item.buyPrice(gold: 10)), 2);
    //        pool.Add((ModContent.ItemType<GoldenCarpScales>(), 5, Item.buyPrice(gold: 15)), 2);
    //        pool.Add((ModContent.ItemType<CelestialEye>(), 5, Item.buyPrice(gold: 25)), 2);
    //        pool.Add((ModContent.ItemType<BrokenStopwatch>(), 10, Item.buyPrice(gold: 25)), 4);
    //        pool.Add((ModContent.ItemType<StellarJade>(), 1, Item.buyPrice(platinum: 1)), 1);
    //        pool.Add((ModContent.ItemType<How2GetRich>(), 1, Item.buyPrice(platinum: 5)), 0.15f);
    //        return pool;
    //    }
    //}

    //private static readonly Dictionary<int, int> _shopItemStocks = new();
    //private static ReadOnlyDictionary<int, int> _originalStock;
    //private static Dictionary<StatType, Item> _jewelStocks = new();
    //private static (int type, int stack, int price) _miscItemInfo;

    //private static bool _setVariableStocks = true;

    private bool _daySwitch = false;

    public override void SetStaticDefaults()
    {
        Main.npcFrameCount[NPC.type] = 26;
        NPCID.Sets.ExtraFramesCount[NPC.type] = 9;
        NPCID.Sets.AttackFrameCount[NPC.type] = 4;
        NPCID.Sets.DangerDetectRange[NPC.type] = 1500;
        NPCID.Sets.AttackType[NPC.type] = 0;
        NPCID.Sets.AttackTime[NPC.type] = 25;
        NPCID.Sets.AttackAverageChance[NPC.type] = 30;

        NPC.Happiness
            .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Like)
            .SetBiomeAffection<HallowBiome>(AffectionLevel.Like)
            .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)
            .SetBiomeAffection<JungleBiome>(AffectionLevel.Hate)
            .SetNPCAffection(NPCID.Clothier, AffectionLevel.Love)
            .SetNPCAffection(NPCID.Painter, AffectionLevel.Like)
            .SetNPCAffection(NPCID.Steampunker, AffectionLevel.Like)
            .SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Dislike)
            .SetNPCAffection(NPCID.PartyGirl, AffectionLevel.Hate);
    }

    public override void SetDefaults()
    {
        NPC.CloneDefaults(NPCID.Guide);
        NPC.townNPC = true;
        NPC.friendly = true;
        NPC.aiStyle = 7;
        NPC.damage = 14;
        NPC.defense = 30;
        NPC.lifeMax = 250;
        NPC.HitSound = SoundID.NPCHit1;
        NPC.DeathSound = SoundID.NPCDeath1;
        NPC.knockBackResist = 0.4f;
        AnimationType = NPCID.Guide;
    }

    public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
        bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
            new FlavorTextBestiaryInfoElement("Somewhat snobby, but incredibly helpful if you want your jewelry to go anywhere. This guy knows his stuff."),
        });
    }

    public override bool CanTownNPCSpawn(int numTownNPCs) => Main.player.Any(x => x.active && x.HasItem(ModContent.ItemType<MajorJewel>()));

    public override List<string> SetNPCNameList() => new() { "Masha", "Madame Bovary", "Lynna", "Tayla", "June", "Tay", "Hastur", "H'aaztre", "Kaiwan", "Fenric" };

    public override string GetChat()
    {
        List<string> dialogue = new List<string>
        {
            "A jewel can turn anyone into a better killing machine!",
            "You'd never catch me DEAD with anything below a Mystical jewel, absolutely not!",
            "Sometimes I wonder, how come I can't put in as many jewels as I want? Then I realize - would be dreadful heavy, no?",
            "I hope you're here to peruse my goods, hm?",
            "\"What are Echoes?\", they ask. Don't worry about it! Buy jewels!"
        };

        return Main.rand.Next(dialogue);
    }

    public override void SetChatButtons(ref string button, ref string button2)
    {
        button = Language.GetTextValue("LegacyInterface.28");
        button2 = "Jewel Menu";
    }

    public override void OnChatButtonClicked(bool firstButton, ref string shopName)
    {
        if (firstButton)
            shopName = "Shop";
        else
        {
            Main.npcChatText = "What would you like me to work on?";
            JewelUISystem.Instance.JewelInterface.SetState(new ChooseJewelMechanicUIState(NPC.whoAmI));
        }
    }

    public override void AddShops()
    {
        //NPCShop shop = new(Type);

        //SetupStockedItem<SparklyDust>(shop, 100);
        //SetupStockedItem<RoughPliers>(shop, 15);
        //SetupStockedItem<NicePliers>(shop, 10);
        //SetupStockedItem<GentlePliers>(shop, 5);
        //SetupStockedItem<SilkenPliers>(shop, 1);
        //shop.Register();

        //Dictionary<int, int> copy = new();

        //foreach (var pair in _shopItemStocks)
        //    copy.Add(pair.Key, pair.Value);

        //_originalStock = new(copy);
        //_setVariableStocks = true;
    }

    private static void SetupStockedItem<T>(NPCShop shop, int stock) where T : ModItem
    {
        //shop.Add(new Item(ModContent.ItemType<T>())
        //{
        //    stack = stock,
        //    buyOnce = true,
        //});

        //_shopItemStocks.Add(ModContent.ItemType<T>(), stock);
    }

    public override void ModifyActiveShop(string shopName, Item[] items)
    {
        //if (!shopName.EndsWith("Shop"))
        //    return;

        //for (int i = 0; i < items.Length; ++i)
        //{
        //    var item = items[i];

        //    if (item is null) 
        //        continue;

        //    if (!_shopItemStocks.ContainsKey(item.type))
        //        continue;

        //    item.stack = _shopItemStocks[item.type];

        //    if (item.stack <= 0)
        //        item.TurnToAir();
        //}

        //AddJewelry(items);
        //AddMiscItem(items);

        //_setVariableStocks = false;
    }

    private void AddMiscItem(Item[] items)
    {
        //if (_setVariableStocks)
        //{
        //    for (int i = _originalStock.Count; i < items.Length; ++i)
        //    {
        //        if (items[i] is null || items[i].IsAir)
        //        {
        //            var (type, stack, price) = MiscPool.Get();
        //            items[i] = new(type)
        //            {
        //                buyOnce = true,
        //                stack = stack,
        //                shopCustomPrice = price
        //            };

        //            _miscItemInfo = (type, stack, price);
        //            break;
        //        }
        //    }
        //}
        //else
        //{
        //    if (_miscItemInfo.type == -1)
        //        return;

        //    for (int i = _originalStock.Count; i < items.Length; ++i)
        //    {
        //        if (items[i] is null || items[i].IsAir)
        //        {
        //            items[i] = new(_miscItemInfo.type) 
        //            { 
        //                stack = _miscItemInfo.stack, 
        //                buyOnce = true,
        //                shopCustomPrice = _miscItemInfo.price
        //            };
        //            break;
        //        }
        //    }
        //}
    }

    //private static void AddJewelry(Item[] items)
    //{
    //    if (_setVariableStocks)
    //    {
    //        int count = 0;

    //        for (int i = _originalStock.Count; i < items.Length; ++i)
    //        {
    //            if (items[i] is null || items[i].IsAir)
    //            {
    //                items[i] = MakeJewel();

    //                while (_jewelStocks.ContainsKey((items[i].ModItem as Jewel).info.Major.Type))
    //                    items[i] = MakeJewel();

    //                _jewelStocks.Add((items[i].ModItem as Jewel).info.Major.Type, items[i]);

    //                if (++count >= 3)
    //                    break;
    //            }
    //        }
    //    }
    //    else
    //    {
    //        int index = 0;

    //        for (int i = _originalStock.Count; i < items.Length; ++i)
    //        {
    //            if (index >= _jewelStocks.Count)
    //                break;

    //            if (items[i] is null || items[i].IsAir)
    //            {
    //                var pair = _jewelStocks.ElementAt(index++);
    //                var firstJewel = pair.Value;
    //                items[i] = firstJewel;
    //            }
    //        }
    //    }
    //}

    private static Item MakeJewel()
    {
        Item item;
        JewelTier tier = BossLootGlobal.HighestBeatenTier;
        item = new Item(JewelryCommon.MajorMinorType());
        Jewel jewel = item.ModItem as Jewel;
        jewel.info.Setup(tier);
        item.buyOnce = true;
        return item;
    }

    public override bool PreAI()
    {
        if (!Main.dayTime)
            _daySwitch = true;

        //if (_daySwitch && Main.dayTime)
        //{
        //    _daySwitch = false;

        //    foreach (var item in _shopItemStocks.Keys)
        //        _shopItemStocks[item] = _originalStock[item];

        //    _setVariableStocks = true;
        //}

        var talkNPC = Main.LocalPlayer.TalkNPC;

        if (talkNPC is not null && talkNPC.whoAmI == NPC.whoAmI && Main.npcShop > 0)
            TrackItems();

        return true;
    }

    private static void TrackItems()
    {
        //Dictionary<int, int> clone = new();
        //List<StatType> foundTypes = new(_jewelStocks.Keys);
        //bool hasMisc = false;

        //foreach (var key in _shopItemStocks.Keys)
        //    clone.Add(key, 0);

        //for (int i = 0; i < Chest.maxItems; ++i)
        //{
        //    Item item = Main.instance.shop[Main.npcShop].item[i];

        //    if (item is null || item.IsAir)
        //        continue;

        //    if (item.type == _miscItemInfo.type && !hasMisc)
        //    {
        //        hasMisc = true;
        //        _miscItemInfo.stack = item.stack;
        //    }

        //    if (item.ModItem is Jewel jewel)
        //    {
        //        if (foundTypes.Contains(jewel.info.Major.Type))
        //            foundTypes.Remove(jewel.info.Major.Type);
        //    }

        //    if (!clone.ContainsKey(item.type))
        //        continue;

        //    clone[item.type] += item.stack;
        //}

        //foreach (var key in clone.Keys)
        //    _shopItemStocks[key] = clone[key];

        //foreach (var item in foundTypes)
        //    _jewelStocks.Remove(item);

        //if (!hasMisc)
        //    _miscItemInfo.type = -1;
    }

    public override void TownNPCAttackStrength(ref int damage, ref float knockback)
    {
        damage = 18;
        knockback = 3f;
    }

    public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
    {
        cooldown = 5;
        randExtraCooldown = 5;
    }

    public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
    {
        projType = ProjectileID.RubyBolt;
        attackDelay = 1;
    }

    public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
    {
        multiplier = 14f;
        randomOffset = 2f;
    }

    public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0 || Main.netMode == NetmodeID.Server)
            return;
    }

    public override void FindFrame(int frameHeight)
    {
        if (!NPC.IsABestiaryIconDummy)
            return;

        NPC.frameCounter += 0.25f;
        if (NPC.frameCounter >= 16)
            NPC.frameCounter = 2;
        else if (NPC.frameCounter < 2)
            NPC.frameCounter = 2;

        int frame = (int)NPC.frameCounter;
        NPC.frame.Y = frame * frameHeight;
    }

    public override ITownNPCProfile TownNPCProfile() => new RuneWizardProfile();
}

public class RuneWizardProfile : ITownNPCProfile
{
    public int RollVariation() => 0;
    public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

    public ReLogic.Content.Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
    {
        var tex = ModContent.GetModNPC(ModContent.NPCType<Lapidarist>()).Texture;

        if (npc.altTexture == 1 && !(npc.IsABestiaryIconDummy && !npc.ForcePartyHatOn))
            return ModContent.Request<Texture2D>(tex + "_Alt_1");

        return ModContent.Request<Texture2D>(tex);
    }

    public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot(ModContent.GetModNPC(ModContent.NPCType<Lapidarist>()).Texture + "_Head");
}