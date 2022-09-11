using Shooter.Model;

namespace Shooter.GameLogic
{
    public sealed class RpgPickup : WeaponWithRollbackAndShootWaitingPickup, IWeaponPickup
    {
        protected override IWeapon Create(IWeapon weapon) => new Rpg(weapon);
        
    }
}