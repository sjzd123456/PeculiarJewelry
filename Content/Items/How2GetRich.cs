using PeculiarJewelry.Content.JewelryMechanic.Misc;
using Terraria.ModLoader.IO;

namespace PeculiarJewelry.Content.Items;

public class How2GetRich : ModItem
{
    public override void SetDefaults()
    {
        Item.Size = new(36, 42);
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.useTime = Item.useAnimation = 20;
        Item.noMelee = true;
        Item.value = Item.buyPrice(platinum: 5);
    }

    public override bool CanUseItem(Player player) => !player.GetModPlayer<RichPlayer>().isRich;

    public override bool? UseItem(Player player)
    {
        player.GetModPlayer<RichPlayer>().isRich = true;
        return true;
    }
}

public class RichPlayer : ModPlayer
{
    public bool isRich = false;

    public override void SaveData(TagCompound tag)
    {
        if (isRich)
            tag.Add("isRich", true);
    }

    public override void LoadData(TagCompound tag)
    {
        if (tag.ContainsKey("isRich"))
            isRich = true;
    }
}
