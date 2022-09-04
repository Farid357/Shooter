using System;
using Shooter.Model;

namespace Shooter.GameLogic
{
    public sealed class WeaponFactoryFromType
    {
        private readonly IWeaponFactory _weaponFactory;
        private readonly WeaponData _weaponData;

        public WeaponFactoryFromType(IWeaponFactory weaponFactory, WeaponData weaponData)
        {
            _weaponFactory = weaponFactory ?? throw new ArgumentNullException(nameof(weaponFactory));
            _weaponData = weaponData ?? throw new ArgumentNullException(nameof(weaponData));
        }
        
        public IWeapon Create(WeaponType type)
        {
            switch (type)
            {
                case WeaponType.Ak74:
                    return _weaponFactory.CreateAk74(_weaponData).Weapon;
                
                case WeaponType.Shotgun:
                    return _weaponFactory.CreateShotgun(_weaponData).Weapon;
                
                case WeaponType.Other:
                    return _weaponFactory.CreateAk74(_weaponData).Weapon;

                default:
                    throw new InvalidOperationException($"{type} not exists!");
            }
        }
    }
}