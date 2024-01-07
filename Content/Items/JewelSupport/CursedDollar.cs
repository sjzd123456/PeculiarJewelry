using PeculiarJewelry.Content.JewelryMechanic.Misc;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using ReLogic.Content;
using System;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace PeculiarJewelry.Content.Items.JewelSupport;

public class CursedDollar : JewelSupportItem
{
    private static float DrawScale => MathF.Sin(Main.GameUpdateCount * 0.16f) * 0.15f + 1f;
    private static Rectangle DrawFrame => new(0, Main.itemAnimations[ModContent.ItemType<CursedDollar>()].Frame * 44, 46, 42);

    private static Asset<Texture2D> Glow;

    public override void SetStaticDefaults()
    {
        Glow = ModContent.Request<Texture2D>(Texture + "_FX");
        Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 8));
    }

    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(silver: 3);
        Item.Size = new(24, 28);
        Item.maxStack = Item.CommonMaxStack;
    }

    public override bool HardOverrideJewelCutChance(JewelInfo info, out float chance)
    {
        chance = 0.5f;
        return true;
    }

    public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        spriteBatch.Draw(Glow.Value, position, DrawFrame, drawColor * DrawScale, 0, new Vector2(23, 21), DrawScale, SpriteEffects.None, 0);
        spriteBatch.Draw(TextureAssets.Item[Type].Value, position + new Vector2(6, 8), null, drawColor, 0, new Vector2(23, 21), 1f, SpriteEffects.None, 0);
    }

    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        Vector2 position = Item.Center - Main.screenPosition;
        spriteBatch.Draw(Glow.Value, position, DrawFrame, lightColor * DrawScale, rotation, new Vector2(23, 21), DrawScale, SpriteEffects.None, 0);
        spriteBatch.Draw(TextureAssets.Item[Type].Value, position + new Vector2(6, 8), null, lightColor, rotation, new Vector2(23, 21), 1f, SpriteEffects.None, 0);
    }
}
