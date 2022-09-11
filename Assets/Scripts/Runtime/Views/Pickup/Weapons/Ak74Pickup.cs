using Shooter.Model;

namespace Shooter.GameLogic
{
    public sealed class Ak74Pickup : WeaponWithRollbackAndShootWaitingPickup, IWeaponPickup
    {
        protected override IWeapon Create(IWeapon weapon) => new Ak74(weapon);
    }
}