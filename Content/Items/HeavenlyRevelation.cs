using PeculiarJewelry.Content.JewelryMechanic.Misc;
using Terraria.ModLoader.IO;

namespace PeculiarJewelry.Content.Items;

public class HeavenlyRevelation : ModItem
{
    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.LifeCrystal);
        Item.Size = new(44, 46);
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.noMelee = true;
        Item.value = Item.buyPrice(platinum: 5);
    }

    public override bool CanUseItem(Player player) => !player.GetModPlayer<RevelationPlayer>().hasReveled;

    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<RevelationPlayer>().hasReveled = true;
        return true;
    }
}

public class RevelationPlayer : ModPlayer
{
    public bool hasReveled = false;

    public override void SaveData(TagCompound tag)
    {
        if (hasReveled)
            tag.Add(nameof(hasReveled), true);
    }

    public override void LoadData(TagCompound tag)
    {
        if (tag.ContainsKey(nameof(hasReveled)))
            hasReveled = true;
    }
}
