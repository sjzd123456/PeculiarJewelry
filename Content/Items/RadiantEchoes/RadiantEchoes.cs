using PeculiarJewelry.Content.JewelryMechanic.Misc;
using System;
using Terraria.GameContent;

namespace PeculiarJewelry.Content.Items.RadiantEchoes;

public abstract class RadiantEchoBase : ModItem
{
    protected abstract Vector2 Size { get; }
    protected virtual Color SparkleColor => Color.White;

    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(gold: 1);
        Item.Size = Size;
        Item.maxStack = Item.CommonMaxStack;
    }

    public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
    {
        var tex = TextureAssets.Item[Type].Value;
        float sine = MathF.Sin(Main.GameUpdateCount * 0.1f) * 0.25f;
        spriteBatch.Draw(tex, position, null, drawColor * (0.15f + sine), 0f, tex.Size() / 2f, (1.25f + sine) * scale, SpriteEffects.None, 0);
    }

    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        var tex = TextureAssets.Item[Type].Value;
        float sine = MathF.Sin(Main.GameUpdateCount * 0.1f) * 0.25f;
        spriteBatch.Draw(tex, Item.Center - Main.screenPosition, null, lightColor * (0.15f + sine), rotation, tex.Size() / 2f, 1.25f + sine, SpriteEffects.None, 0);
    }

    public override void Update(ref float gravity, ref float maxFallSpeed)
    {
        Lighting.AddLight(Item.Center, new Vector3(0.3f));

        if (Main.rand.NextBool(60))
        {
            var pos = Item.position + new Vector2(Main.rand.NextFloat(Item.width), Main.rand.NextFloat(Item.height));
            var dust = Dust.NewDustPerfect(pos, DustID.ShimmerSpark, Vector2.Zero, 0, SparkleColor);
            dust.noGravity = true;
        }
    }
}

public class RadiantEcho : RadiantEchoBase
{
    protected override Vector2 Size => new(24, 28);
}

public class DoubleRadiantEcho : RadiantEchoBase
{
    protected override Vector2 Size => new(28, 34);
}

public class TripleRadiantEcho : RadiantEchoBase
{
    protected override Vector2 Size => new(28, 38);
}

public class QuadRadiantEcho : RadiantEchoBase
{
    protected override Vector2 Size => new(28, 38);
}

public class QuintRadiantEcho : RadiantEchoBase
{
    protected override Vector2 Size => new(40, 42);
}

public class SextupleRadiantEcho : RadiantEchoBase
{
    protected override Vector2 Size => new(20, 34);
    protected override Color SparkleColor => new(117, 201, 239);
}

public class SeptRadiantEcho : RadiantEchoBase
{
    protected override Vector2 Size => new(32, 36);
    protected override Color SparkleColor => new(117, 201, 239);
}

public class TranscendantEcho : ModItem
{
    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.buyPrice(platinum: 1);
        Item.Size = new(40, 42);
        Item.maxStack = Item.CommonMaxStack;
    }
}