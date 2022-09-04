using Shooter.Model;

namespace Shooter.GameLogic
{
    public interface IWeaponFactory
    {
        public (IWeapon Weapon, IWeaponWithRollback Rollback) CreateShotgun(WeaponData data);
        public (IWeapon Weapon, IWeaponWithRollback Rollback) CreateAk74(WeaponData data);
    }
}