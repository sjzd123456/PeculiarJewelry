using Terraria.DataStructures;

namespace PeculiarJewelry.Content.JewelryMechanic.GrindstoneSystem;

internal interface IGrindableItem
{
    public bool GrindstoneUse(int i, int j, IEntitySource source);
    public bool CanGrind(int i, int j) => true;
}
