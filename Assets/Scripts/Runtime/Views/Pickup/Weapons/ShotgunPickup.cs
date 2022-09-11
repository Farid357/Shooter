using Shooter.Model;

namespace Shooter.GameLogic
{
    public sealed class ShotgunPickup : WeaponWithRollbackAndShootWaitingPickup, IWeaponPickup
    {
        protected override IWeapon Create(IWeapon weapon) => new Shotgun(weapon);
        
    }
}