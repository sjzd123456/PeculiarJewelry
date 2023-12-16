using Terraria.DataStructures;

namespace PeculiarJewelry.Content.JewelryMechanic;

internal class TriggerGlobalProjectile : GlobalProjectile
{
    public override bool InstancePerEntity => true;

    public bool FromTrigger => _spawnedFromTrigger;

    private bool _spawnedFromTrigger = false;

    public override void OnSpawn(Projectile projectile, IEntitySource source)
    {
        if (source is EntitySource_Misc misc)
            _spawnedFromTrigger = misc.Context.StartsWith("JewelryTrigger:");
    }
}
