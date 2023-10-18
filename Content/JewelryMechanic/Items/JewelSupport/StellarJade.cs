using PeculiarJewelry.Content.JewelryMechanic.Misc;
using PeculiarJewelry.Content.JewelryMechanic.Stats;
using System;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace PeculiarJewelry.Content.JewelryMechanic.Items.JewelSupport;

public class StellarJade : JewelSupportItem, ISetSupportItem
{
    public override void SetDefaults()
    {
        Item.rare = ModContent.RarityType<JewelRarity>();
        Item.value = Item.sellPrice(silver: 3);
        Item.Size = new(28, 22);
        Item.maxStack = Item.CommonMaxStack;
    }

    public override void Update(ref float gravity, ref float maxFallSpeed)
    {
        Lighting.AddLight(Item.Center, new Vector3(0.1f, 0.4f, 0.2f));

        if (Main.rand.NextBool(3))
        {
            Vector2 dir = Main.rand.NextVector2Circular(4, 4);
            Dust.NewDust(Item.Center, 1, 1, DustID.GreenFairy, Item.velocity.X + dir.X, Item.velocity.Y + dir.Y);
        }
    }

    public override bool HardOverrideJewelCutChance(JewelInfo info, out float chance)
    {
        chance = 1f;
        return true;
    }
}

public class StellarJadeProjectile : ModProjectile
{
    public override string Texture => base.Texture.Replace("Projectile", "");

    public override void SetDefaults()
    {
        Projectile.Size = new Vector2(34, 34);
        Projectile.friendly = true;
    }

    public override bool OnTileCollide(Vector2 oldVelocity) => true;

    public override void AI()
    {
        Projectile.rotation += 0.1f;
        Lighting.AddLight(Projectile.Center, new Vector3(0.1f, 0.4f, 0.2f) * 2);

        if (Main.rand.NextBool(30))
            SoundEngine.PlaySound(SoundID.Item4 with { Volume = 0.2f, Pitch = 0.75f, PitchVariance = 0.25f }, Projectile.Center);

        if (Main.rand.NextBool(3))
            Dust.NewDust(Projectile.Center, 1, 1, DustID.GreenFairy, Projectile.velocity.X, Projectile.velocity.Y);
    }

    public override void OnKill(int timeLeft) => Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.Center, ModContent.ItemType<StellarJade>());

    public override bool PreDraw(ref Color lightColor)
    {
        var color = Lighting.GetColor(Projectile.Center.ToTileCoordinates());
        var extraColor = Lighting.GetColor(Projectile.Center.ToTileCoordinates(), Color.Aquamarine);
        var tex = TextureAssets.Projectile[Type].Value;
        var pos = Projectile.Center - Main.screenPosition;
        var rot = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        var extraTex = TextureAssets.Extra[ExtrasID.FallingStar].Value;

        float Sine(float offset) => MathF.Sin(Projectile.timeLeft * 0.1f + offset) * 0.2f;
        Vector2 SinePos(float offset) => new(Sine(offset) * 4, 0);

        Main.spriteBatch.Draw(extraTex, pos + SinePos(0.2f), null, extraColor * (0.35f + Sine(0.2f)), rot, tex.Size() / 2f, 1.5f, SpriteEffects.None, 0);
        Main.spriteBatch.Draw(extraTex, pos + SinePos(1.28f), null, extraColor * (0.35f + Sine(1.28f)), rot, tex.Size() / 2f, 1.75f, SpriteEffects.None, 0);
        Main.spriteBatch.Draw(extraTex, pos + SinePos(2.2f), null, extraColor * (0.25f + Sine(2.2f)), rot, tex.Size() / 2f, 1.5f, SpriteEffects.None, 0);
        Main.spriteBatch.Draw(extraTex, pos + SinePos(3.2f), null, extraColor * (0.5f + Sine(3.2f)), rot, tex.Size() / 2f, 1f, SpriteEffects.None, 0);
        Main.spriteBatch.Draw(tex, pos - Projectile.velocity * 2, null, color * 0.5f, Projectile.rotation, tex.Size() / 2f, 1.2f, SpriteEffects.None, 0);
        Main.spriteBatch.Draw(tex, pos - Projectile.velocity * 4, null, color * 0.25f, Projectile.rotation, tex.Size() / 2f, 1.5f, SpriteEffects.None, 0);
        return true;
    }

    private class StellarJadeReplacementGlobalProjectile : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation) => entity.type == ProjectileID.FallingStar;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            if (Main.rand.NextBool(150))
            {
                Vector2 center = projectile.Center;
                float velocity = projectile.velocity.Y;
                projectile.SetDefaults(ModContent.ProjectileType<StellarJadeProjectile>());
                projectile.netUpdate = true;
                projectile.velocity.Y = velocity;
                projectile.Center = center;
            }
        }
    }
}
