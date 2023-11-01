using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System.Linq;
using Terraria.Utilities;

namespace PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses.Bonuses;

internal class GoldBonus : BaseMaterialBonus
{
    public override string MaterialKey => "Gold";

    public override float EffectBonus(Player player, StatType statType)
    {
        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 1)
            return 1.15f;
        return 1f;
    }

    public override void StaticBonus(Player player, bool firstSet)
    {
        player.luck += 0.5f;

        int count = player.GetModPlayer<MaterialPlayer>().MaterialCount(MaterialKey);

        if (count >= 3)
            player.GetModPlayer<GoldBonusPlayer>().threeSet = true;
    }

    // Needs 3-Set, 5-Set

    class GoldBonusPlayer : ModPlayer
    {
        private static readonly int[] Woods = new int[] { ItemID.Wood, ItemID.AshWood, ItemID.BorealWood, ItemID.RichMahogany, ItemID.BorealWood, ItemID.Ebonwood, 
            ItemID.PalmWood, ItemID.Shadewood };

        private static readonly int[] Ore = new int[] { ItemID.GoldOre, ItemID.CopperOre, ItemID.TinOre, ItemID.IronOre, ItemID.LeadOre, ItemID.SilverOre,
            ItemID.TungstenOre, ItemID.PlatinumOre };

        internal bool threeSet = false;

        public override void ResetEffects() => threeSet = false;

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (threeSet)
            {
                if (item.pick > 0 && Main.rand.NextBool(3))
                    Item.NewItem(Player.GetSource_OnHit(target), target.Hitbox, Main.rand.Next(Ore));

                if (item.axe > 0 && Main.rand.NextBool(3))
                    Item.NewItem(Player.GetSource_OnHit(target), target.Hitbox, Main.rand.Next(Woods));
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (threeSet)
            {
                if (!Main.rand.NextBool(10) || target.life > 0)
                    return;

                 WeightedRandom<int> type = new();

                if (Player.ZoneJungle)
                    type.Add(ItemID.Moonglow);

                if (Player.ZoneCorrupt || Player.ZoneCrimson)
                    type.Add(ItemID.Deathweed);

                if (Player.ZoneDesert)
                    type.Add(ItemID.Waterleaf);

                if (Player.ZoneRockLayerHeight)
                    type.Add(ItemID.Blinkroot);

                if (Player.ZoneUnderworldHeight)
                    type.Add(ItemID.Fireblossom);

                if (Player.ZoneSnow)
                    type.Add(ItemID.Shiverthorn);

                if (type.elements.Any())
                    Item.NewItem(Player.GetSource_OnHit(target), target.Hitbox, type);
            }
        }
    }
}
