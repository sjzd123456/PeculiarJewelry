using PeculiarJewelry.Content.JewelryMechanic.Stats;
using PeculiarJewelry.Content.JewelryMechanic.Stats.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ModLoader.IO;

namespace PeculiarJewelry.Content.Items;

public class SubShard : ModItem
{
    private Rectangle Source => new(_frame * 24, 0, 22, 30);

    internal JewelStat stat = new(StatType.Absolution);

    int _frame = 0;

    public override void SetDefaults()
    {
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(silver: 3);
        Item.Size = new(22, 30);
        Item.maxStack = 1;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var name = tooltips.First(x => x.Name == "ItemName");
        name.OverrideColor = stat.Get().Color;
        tooltips.Add(new TooltipLine(Mod, "StatType", stat.GetDescription(Main.LocalPlayer, false)) { OverrideColor = stat.Get().Color, });
    }

    public override void OnSpawn(IEntitySource source) => _frame = Main.rand.Next(3);

    public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color iCol, Vector2 origin, float scale)
    {
        Vector2 adj = new(3, 1);
        spriteBatch.Draw(TextureAssets.Item[Type].Value, position, Source, stat.Get().Color, 0f, origin / adj, scale * 3, SpriteEffects.None, 0);
        return false;
    }

    public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
    {
        Color col = lightColor.MultiplyRGB(stat.Get().Color);
        spriteBatch.Draw(TextureAssets.Item[Type].Value, Item.Center - Main.screenPosition, Source, col, rotation, Item.Size / 2f, scale, SpriteEffects.None, 0);
        return false;
    }

    public override void SaveData(TagCompound tag) => tag.Add(nameof(stat), stat.SaveAs());
    public override void LoadData(TagCompound tag) => stat = JewelIO.LoadStat(tag.GetCompound(nameof(stat)));
}
