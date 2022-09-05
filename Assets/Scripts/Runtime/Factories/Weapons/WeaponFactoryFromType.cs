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
            return type switch
            {
                WeaponType.Ak74 => _weaponFactory.CreateAk74(_weaponData).Weapon,
                WeaponType.Shotgun => _weaponFactory.CreateShotgun(_weaponData).Weapon,
                WeaponType.Other => _weaponFactory.CreateAk74(_weaponData).Weapon,
                _ => throw new InvalidOperationException($"{type} not exists!")
            };
        }
    }
}