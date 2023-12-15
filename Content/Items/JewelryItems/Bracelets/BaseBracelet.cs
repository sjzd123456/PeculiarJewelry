using PeculiarJewelry.Content.JewelryMechanic.MaterialBonuses;
using ReLogic.Content;
using System.Linq;
using Terraria.GameContent;

namespace PeculiarJewelry.Content.Items.JewelryItems.Bracelets;

[AutoloadEquip(EquipType.HandsOn)]
[Autoload(false)]
public class BaseBracelet : BasicJewelry
{
    static Asset<Texture2D> _jewels;
    static Asset<Texture2D> _jewelsEquip;

    protected override bool CloneNewInstances => true;

    public override string Name => _name;
    public override string MaterialCategory => _category;

    protected int _material;
    protected string _category = string.Empty;
    protected string _name = string.Empty;
    protected bool _isHardmode = false;

    public BaseBracelet(string name, string category, int mat, bool isHardmode)
    {
        _name = name;
        _category = category;
        _material = mat;
        _isHardmode = isHardmode;
    }

    public override ModItem Clone(Item newEntity)
    {
        var clone = base.Clone(newEntity) as BaseBracelet;
        clone._material = _material;
        clone._category = _category;
        clone._name = _name;
        return clone;
    }

    public override void SetStaticDefaults()
    {
        _jewels ??= Mod.Assets.Request<Texture2D>("Content/Items/JewelryItems/Bracelets/BraceletJewel");
        _jewelsEquip ??= Mod.Assets.Request<Texture2D>("Content/Items/JewelryItems/Bracelets/BraceletJewel_HandsOn");
    }

    public override void Unload() => _jewels = null;

    protected override void Defaults()
    {
        Item.width = 34;
        Item.height = 30;
        Item.accessory = true;
    }

    protected override void EquipEffect(Player player, bool isVanity = false)
    {
        if (Info.Any())
            player.GetModPlayer<MaterialPlayer>().SetEquip(EquipType.HandsOn, new MaterialPlayer.EquipLayerInfo(GetDisplayColor(), _jewelsEquip.Value));
    }

    public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color i, Vector2 origin, float scale)
    {
        if (Info.Any())
            spriteBatch.Draw(_jewels.Value, position, frame, GetDisplayColor(), 0f, origin, scale, SpriteEffects.None, 0);
    }

    public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
    {
        if (Info.Any())
            spriteBatch.Draw(_jewels.Value, Item.Center - Main.screenPosition, null, GetDisplayColor(), rotation, _jewels.Size() / 2f, scale, SpriteEffects.None, 0);
    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(_material, 2)
            .AddTile(_isHardmode ? TileID.MythrilAnvil : TileID.Anvils)
            .Register();
    }

    public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
    {
        if (equippedItem.ModItem is BaseBracelet)
            return incomingItem.ModItem is not BaseBracelet;

        return true;
    }
}

internal class BraceletLoader : ILoadable
{
    public void Load(Mod mod)
    {
        // Prehardmode
        AddBrooch("Copper", ItemID.CopperBar);
        AddBrooch("Tin", ItemID.TinBar);
        AddBrooch("Iron", ItemID.IronBar);
        AddBrooch("Lead", ItemID.LeadBar);
        AddBrooch("Silver", ItemID.SilverBar);
        AddBrooch("Tungsten", ItemID.TungstenBar);
        AddBrooch("Gold", ItemID.GoldBar);
        AddBrooch("Platinum", ItemID.PlatinumBar);
        AddBrooch("Demonite", ItemID.DemoniteBar);
        AddBrooch("Crimtane", ItemID.CrimtaneBar);
        AddBrooch("Meteorite", ItemID.MeteoriteBar);
        AddBrooch("Hellstone", ItemID.HellstoneBar);

        //// Hardmode
        AddBrooch("Cobalt", ItemID.CobaltBar);
        AddBrooch("Palladium", ItemID.PalladiumBar);
        AddBrooch("Mythril", ItemID.MythrilBar);
        AddBrooch("Orichalcum", ItemID.OrichalcumBar);
        AddBrooch("Adamantite", ItemID.AdamantiteBar);
        AddBrooch("Titanium", ItemID.TitaniumBar);
        AddBrooch("Hallowed", ItemID.HallowedBar);
        AddBrooch("Chlorophyte", ItemID.ChlorophyteBar);
        AddBrooch("Beetle", ItemID.BeetleHusk);
        AddBrooch("Shroomite", ItemID.ShroomiteBar);
        AddBrooch("Spectre", ItemID.SpectreBar);
        AddBrooch("Spooky", ItemID.SpookyWood);
        AddBrooch("Luminite", ItemID.LunarBar);
    }

    private static bool AddBrooch(string category, int material, bool isHardmode = false)
        => ModContent.GetInstance<PeculiarJewelry>().AddContent(new BaseBracelet(category + "Bracelet", category, material, isHardmode));

    public void Unload()
    {
    }
}